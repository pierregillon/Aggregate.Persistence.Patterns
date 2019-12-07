using System;
using Patterns.EventSourcing.Domain.Base;

namespace Patterns.EventSourcing.Domain.Events
{
    public class OrderDeleted : IDomainEvent
    {
        public Guid AggregateId { get; }

        public OrderDeleted(Guid orderId)
        {
            AggregateId = orderId;
        }
    }
}