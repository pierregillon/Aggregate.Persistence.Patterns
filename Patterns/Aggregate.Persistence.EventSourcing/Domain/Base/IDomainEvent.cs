using System;

namespace Aggregate.Persistence.EventSourcing.Domain.Base
{
    public interface IDomainEvent
    {
        Guid AggregateId { get; }
    }
}