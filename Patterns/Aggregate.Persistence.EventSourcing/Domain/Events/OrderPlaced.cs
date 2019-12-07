using System;
using Patterns.EventSourcing.Domain.Base;

namespace Patterns.EventSourcing.Domain.Events
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