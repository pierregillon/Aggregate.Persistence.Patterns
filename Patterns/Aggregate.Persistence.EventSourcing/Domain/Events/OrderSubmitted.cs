using System;
using Aggregate.Persistence.EventSourcing.Domain.Base;

namespace Aggregate.Persistence.EventSourcing.Domain.Events
{
    public class OrderSubmitted : IDomainEvent
    {
        public Guid AggregateId { get; }
        public DateTime SubmitDate { get; }

        public OrderSubmitted(Guid id, DateTime submitDate)
        {
            AggregateId = id;
            SubmitDate = submitDate;
        }
    }
}