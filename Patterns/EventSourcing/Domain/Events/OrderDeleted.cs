using System;

namespace Patterns.EventSourcing.Domain.Events
{
    public class OrderDeleted : IDomainEvent
    {
        public Guid AggregateId { get; private set; }

        public OrderDeleted(Guid orderId)
        {
            AggregateId = orderId;
        }
    }
}