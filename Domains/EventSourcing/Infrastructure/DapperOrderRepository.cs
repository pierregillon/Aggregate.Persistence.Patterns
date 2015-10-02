using System;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Domain.Base.Infrastructure;
using Domains.EventSourcing.Domain;
using Domains.EventSourcing.Domain.Events;
using Newtonsoft.Json;

namespace Domains.EventSourcing.Infrastructure
{
    public class DapperOrderRepository : IOrderRepository
    {
        public Order Get(Guid id)
        {
            using (var connection = new SqlConnection(SqlConnectionLocator.LocalhostSqlExpress())) {
                const string query = @"SELECT AggregateId, CreationDate, Content, Name FROM OrderEvent WHERE AggregateId = @id";

                var domainEvents = connection
                    .Query<OrderEvent>(query, new {id})
                    .OrderBy(x => x.CreationDate)
                    .ToArray()
                    .Select(ConvertToDomainEvent)
                    .ToArray(); ;

                var order = new Order();
                order.Replay(domainEvents);
                return order;
            }
        }
        public void Add(Order order)
        {
            var domainEvents = order.GetUncommittedEvents();
            var persistedEvents = domainEvents.Select(ConvertToPersistantEvent);
            using (var connection = new SqlConnection(SqlConnectionLocator.LocalhostSqlExpress())) {
                const string query = "INSERT INTO OrderEvent (AggregateId, CreationDate, Content, Name) " +
                                     "VALUES(@AggregateId, @CreationDate, @Content, @Name)";
                connection.Execute(query, persistedEvents);
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