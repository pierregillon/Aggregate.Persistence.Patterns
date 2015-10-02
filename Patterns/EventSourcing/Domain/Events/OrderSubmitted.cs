using System;

namespace Patterns.EventSourcing.Domain.Events
{
    public class OrderSubmitted : IDomainEvent
    {
        public Guid AggregateId { get; private set; }
        public DateTime SubmitDate { get; private set; }

        public OrderSubmitted(Guid id, DateTime submitDate)
        {
            AggregateId = id;
            SubmitDate = submitDate;
        }
    }
}