using System;
using Aggregate.Persistence.EventSourcing.Domain.Base;

namespace Aggregate.Persistence.EventSourcing.Domain.Events
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