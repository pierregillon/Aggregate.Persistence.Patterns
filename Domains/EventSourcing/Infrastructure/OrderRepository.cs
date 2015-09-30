using System;
using System.Linq;
using Domains.EventSourcing.Domain;
using Domains.EventSourcing.Domain.Events;
using Domains.EventSourcing.Infrastructure.EntityFramework;
using Newtonsoft.Json;

namespace Domains.EventSourcing.Infrastructure
{
    public class OrderRepository : IOrderRepository
    {
        public Order Get(Guid id)
        {
            using (var dataContext = new DataContext()) {
                var domainEvents = dataContext
                    .Set<OrderEvent>()
                    .Where(x => x.AggregateId == id)
                    .OrderBy(x => x.CreationDate)
                    .ToArray()
                    .Select(ConvertToDomainEvent)
                    .ToArray();
                
                var order = new Order();
                order.Replay(domainEvents);
                return order;
            }
        }
        public void Add(Order order)
        {
            var domainEvents = order.GetUncommittedEvents();
            var persistedEvents = domainEvents.Select(ConvertToPersistantEvent);
            using (var dataContext = new DataContext()) {
                dataContext.Set<OrderEvent>().AddRange(persistedEvents);
                dataContext.SaveChanges();
            }
        }

        // ----- Utils

        private static OrderEvent ConvertToPersistantEvent(IDomainEvent domainEvent)
        {
            return new OrderEvent
            {
                AggregateId = domainEvent.AggregateId,
                CreationDate = DateTime.Now, 
                Name = domainEvent.GetType().ToString(),
                Content = JsonConvert.SerializeObject(domainEvent)
            };
        }
        private IDomainEvent ConvertToDomainEvent(OrderEvent persistedEvent)
        {
            var type = GetType().Assembly.GetType(persistedEvent.Name);
            return (IDomainEvent)JsonConvert.DeserializeObject(persistedEvent.Content, type);
        }
    }
}