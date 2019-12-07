using System;
using Aggregate.Persistence.EventSourcing.Domain.Base;

namespace Aggregate.Persistence.EventSourcing.Domain.Events
{
    public class OrderPlaced : IDomainEvent
    {
        public Guid AggregateId { get; }

        public OrderPlaced(Guid aggregateId)
        {
            AggregateId = aggregateId;
        }
    }
}