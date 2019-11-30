using System;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Newtonsoft.Json;
using Patterns.Contract.Infrastructure;
using Patterns.EventSourcing.Domain;
using Patterns.EventSourcing.Domain.Events;

namespace Patterns.EventSourcing.Infrastructure
{
    public class DapperOrderRepository : IOrderRepository
    {
        public Order Get(Guid id)
        {
            using (var connection = new SqlConnection(SqlConnectionLocator.LocalhostSqlExpress())) {
                var domainEvents = connection
                    .Query<OrderEvent>(SqlQueries.SelectOrderEventQuery, new {id})
                    .OrderBy(x => x.CreationDate)
                    .ToArray()
                    .Select(ConvertToDomainEvent)
                    .ToArray();

                var order = new Order();
                order.Replay(domainEvents);
                if (order.IsDeleted) {
                    return null;
                }
                return order;
            }
        }

        public void Add(Order order)
        {
            SaveUncommitedEvents(order);
        }
        public void Update(Order order)
        {
            SaveUncommitedEvents(order);
        }
        public void Delete(Guid orderId)
        {
            var @event = new OrderDeleted(orderId);
            var eventToPersist = ConvertToPersistentEvent(@event);
            using (var connection = new SqlConnection(SqlConnectionLocator.LocalhostSqlExpress())) {
                connection.Execute(SqlQueries.InsertOrderEventQuery, eventToPersist);
            }
        }

        // ----- Internal logics
        private static void SaveUncommitedEvents(Order order)
        {
            var domainEvents = order.GetUncommittedEvents();
            var persistedEvents = domainEvents.Select(ConvertToPersistentEvent);
            using (var connection = new SqlConnection(SqlConnectionLocator.LocalhostSqlExpress())) {
                connection.Execute(SqlQueries.InsertOrderEventQuery, persistedEvents);
            }
        }

        // ----- Utils
        private static OrderEvent ConvertToPersistentEvent(IDomainEvent domainEvent)
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
            return (IDomainEvent) JsonConvert.DeserializeObject(persistedEvent.Content, type);
        }
    }
}