using System;

namespace Patterns.EventSourcing.Domain.Events
{
    public interface IDomainEvent
    {
        Guid AggregateId { get; }
    }
}