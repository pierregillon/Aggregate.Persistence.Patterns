using System;

namespace Domains.EventSourcing.Domain
{
    public class OrderCreated : IDomainEvent
    {
        public Guid AggregateId { get; private set; }
        public OrderCreated(Guid aggregateId)
        {
            AggregateId = aggregateId;
        }
    }
}