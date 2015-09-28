using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Base;
using Domains.ModelInterface.Domain;
using Domains.ModelInterface.Infrastructure;
using Domains.ModelInterface.Infrastructure.EntityFramework;
using NFluent;
using Xunit;

namespace Domains.Tests.Persistance
{
    public class OrderWithModelInterfacePatternTests
    {
        [Fact]
        public void AddOrderInDatabase()
        {
            var order = new Order();
            order.AddProduct(Product.Computer, 1);
            order.AddProduct(Product.Jacket, 2);
            order.Submit();

            var orderRepository = new OrderRepository();
            orderRepository.Add(order);

            using (var dataContext = new DataContext()) {
                var orderInDatabase = dataContext.Set<PersistantOrder>().Single(x => x.Id == order.Id);
                Check.That(orderInDatabase.OrderStatus).IsEqualTo(OrderStatus.Submitted);
                Check.That(orderInDatabase.SubmitDate.Value.ToLongDateString()).IsEqualTo(DateTime.Now.ToLongDateString());
                Check.That(orderInDatabase.TotalCost).IsEqualTo(1089);

                var lines = dataContext.Set<PersistantOrderLine>().Where(x => x.OrderId == order.Id).ToArray();
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

            using (var dataContext = new DataContext()) {
                var orderState = new PersistantOrder
                {
                    Id = guid,
                    OrderStatus = OrderStatus.Draft,
                    TotalCost = 688.00,
                    Lines = new List<PersistantOrderLine>()
                };
                var orderLineState = new PersistantOrderLine
                {
                    Order = orderState,
                    Product = Product.Computer,
                    Quantity = 1
                };
                dataContext.Set<PersistantOrder>().Add(orderState);
                dataContext.Set<PersistantOrderLine>().Add(orderLineState);
                dataContext.SaveChanges();
            }

            var orderRepository = new OrderRepository();
            var order = orderRepository.Get(guid);

            Check.That(order.Id).IsEqualTo(guid);
            Check.That(order.SubmitDate).IsNull();
            Check.That(order.TotalCost).IsEqualTo(688.00);
            Check.That(order.GetQuantity(Product.Computer)).IsEqualTo(1);
        }
    }
}
