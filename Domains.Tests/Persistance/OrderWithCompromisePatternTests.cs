using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Base;
using Domains.Compromise.Infrastructure;
using Domains.Compromise.Infrastructure.EntityFramework;
using NFluent;
using Xunit;
using Order = Domains.Compromise.Domain.Order;
using OrderLine = Domains.Compromise.Domain.OrderLine;

namespace Domains.Tests.Persistance
{
    public class OrderWithCompromisePatternTests
    {
        [Fact]
        public void AddOrderInDatabase()
        {
            var order = new Order();
            order.AddProduct(Product.Computer, 1);
            order.AddProduct(Product.Jacket, 2);
            order.Submit();

            var orderRepository = new EntityFrameworkOrderRepository();
            orderRepository.Add(order);

            using (var dataContext = new DataContext()) {
                var orderInDatabase = dataContext.Set<Order>().Single(x => x.Id == order.Id);
                Check.That(orderInDatabase.OrderStatus).IsEqualTo(OrderStatus.Submitted);
                Check.That(orderInDatabase.SubmitDate.Value.ToLongDateString()).IsEqualTo(DateTime.Now.ToLongDateString());
                Check.That(orderInDatabase.TotalCost).IsEqualTo(1089);

                var lines = dataContext.Set<OrderLine>().Where(x => x.OrderId == order.Id).ToArray();
                Check.That(lines[0].Product).IsEqualTo(Product.Jacket);
                Check.That(lines[0].Quantity).IsEqualTo(2);
                Check.That(lines[1].Product).IsEqualTo(Product.Computer);
                Check.That(lines[1].Quantity).IsEqualTo(1);
            }
        }

        [Fact]
        public void LoadOrderFromDatabase()
        {
            var guid = Guid.NewGuid();

            using (var dataContext = new DataContext())
            {
                var orderState = new Order
                {
                    Id = guid,
                    OrderStatus = OrderStatus.Draft,
                    TotalCost = 688.00,
                    Lines = new List<OrderLine>()
                };
                var orderLineState = new OrderLine
                {
                    Order = orderState,
                    Product = Product.Computer,
                    CreationDate = DateTime.Now.RoundToSecond(),
                    Quantity = 1
                };
                dataContext.Set<Order>().Add(orderState);
                dataContext.Set<OrderLine>().Add(orderLineState);
                dataContext.SaveChanges();
            }

            var orderRepository = new EntityFrameworkOrderRepository();
            var order = orderRepository.Get(guid);

            Check.That(order.Id).IsEqualTo(guid);
            Check.That(order.OrderStatus).IsEqualTo(OrderStatus.Draft);
            Check.That(order.SubmitDate).IsNull();
            Check.That(order.TotalCost).IsEqualTo(688.00);
            Check.That(order.GetQuantity(Product.Computer)).IsEqualTo(1);
        }
    }
}
