using System;
using Patterns.EventSourcing.Domain.Base;

namespace Patterns.EventSourcing.Domain.Events
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