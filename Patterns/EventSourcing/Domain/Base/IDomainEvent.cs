using System;

namespace Patterns.EventSourcing.Domain.Base
{
    public interface IDomainEvent
    {
        Guid AggregateId { get; }
    }
}