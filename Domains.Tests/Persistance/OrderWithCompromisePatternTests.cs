using System;
using System.Collections.Generic;
using System.Linq;
using ClassLibrary1;
using Domains.Compromise.Domain;
using Domains.Compromise.Infrastructure;
using Domains.Compromise.Infrastructure.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;

namespace Domains.Tests.Persistance
{
    [TestClass]
    public class OrderWithCompromisePatternTests
    {
        [TestMethod]
        public void AddOrderInDatabase()
        {
            var order = new Order();
            order.AddProduct(Product.Computer, 1);
            order.AddProduct(Product.Jacket, 2);
            order.Submit();

            var orderRepository = new OrderRepository();
            orderRepository.Add(order);

            using (var dataContext = new DataContext()) {
                var orderInDatabase = dataContext.Set<Order>().Single(x => x.Id == order.Id);
                Check.That(orderInDatabase.OrderStatus).IsEqualTo(OrderStatus.Submitted);
                Check.That(orderInDatabase.SubmitDate.ToLongDateString()).IsEqualTo(DateTime.Now.ToLongDateString());
                Check.That(orderInDatabase.TotalCost).IsEqualTo(1089);

                var lines = dataContext.Set<OrderLine>().Where(x => x.OrderId == order.Id).ToArray();
                Check.That(lines[0].Product).IsEqualTo(Product.Jacket);
                Check.That(lines[0].Quantity).IsEqualTo(2);
                Check.That(lines[1].Product).IsEqualTo(Product.Computer);
                Check.That(lines[1].Quantity).IsEqualTo(1);
            }
        }

        [TestMethod]
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
                    Quantity = 1
                };
                dataContext.Set<Order>().Add(orderState);
                dataContext.Set<OrderLine>().Add(orderLineState);
                dataContext.SaveChanges();
            }

            var orderRepository = new OrderRepository();
            var order = orderRepository.Get(guid);

            Check.That(order.Id).IsEqualTo(guid);
            Check.That(order.OrderStatus).IsEqualTo(OrderStatus.Draft);
            Check.That(order.SubmitDate).IsEqualTo(default(DateTime));
            Check.That(order.TotalCost).IsEqualTo(688.00);
            Check.That(order.GetQuantity(Product.Computer)).IsEqualTo(1);
        }
    }
}
