using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Base;
using Domains.EventSourcing.Domain;
using Domains.EventSourcing.Infrastructure;
using Domains.EventSourcing.Infrastructure.EntityFramework;
using NFluent;
using Xunit;

namespace Domains.Tests.Persistance
{
    public class OrderWithEventSourcingPatternTests
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
                var events = dataContext.Set<OrderEvent>().Where(x => x.AggregateId == order.Id).ToArray();
                Check.That(events.Count()).IsEqualTo(4);
            }
        }

        [Fact]
        public void LoadOrderFromDatabase()
        {
            var guid = Guid.NewGuid();

            using (var dataContext = new DataContext()) {
                dataContext.Set<OrderEvent>().Add(new OrderEvent
                {
                    AggregateId = guid,
                    CreationDate = DateTime.Now,
                    Name = typeof (OrderCreated).ToString(),
                    Content = "{\"AggregateId\":\"" + guid + "\"}"
                });
                dataContext.Set<OrderEvent>().Add(new OrderEvent
                {
                    AggregateId = guid,
                    CreationDate = DateTime.Now,
                    Name = typeof(ProductAdded).ToString(),
                    Content = "{\"AggregateId\":\"" + guid + "\",\"Product\":2,\"Quantity\":1}"
                });
                dataContext.Set<OrderEvent>().Add(new OrderEvent
                {
                    AggregateId = guid,
                    CreationDate = DateTime.Now,
                    Name = typeof(ProductAdded).ToString(),
                    Content = "{\"AggregateId\":\"" + guid + "\",\"Product\":1,\"Quantity\":2}"
                });
                dataContext.Set<OrderEvent>().Add(new OrderEvent
                {
                    AggregateId = guid,
                    CreationDate = DateTime.Now,
                    Name = typeof(OrderSubmitted).ToString(),
                    Content = "{\"AggregateId\":\"" + guid + "\",\"SubmitDate\":\"2015-09-30T00:03:54.4225513+02:00\"}"
                });
                dataContext.SaveChanges();
            }

            var orderRepository = new OrderRepository();
            var order = orderRepository.Get(guid);

            Check.That(order.Id).IsEqualTo(guid);
            Check.That(order.SubmitDate).IsEqualTo(DateTime.Parse("2015-09-30T00:03:54.4225513+02:00"));
            Check.That(order.TotalCost).IsEqualTo(1089);
            Check.That(order.GetQuantity(Product.Jacket)).IsEqualTo(2);
            Check.That(order.GetQuantity(Product.Computer)).IsEqualTo(1);
        }
    }
}