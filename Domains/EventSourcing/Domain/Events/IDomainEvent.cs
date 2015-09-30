using System;

namespace Domains.EventSourcing.Domain.Events
{
    public interface IDomainEvent
    {
        Guid AggregateId { get; }
    }
}