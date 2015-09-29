using System;

namespace Domains.EventSourcing.Domain
{
    public interface IDomainEvent
    {
        Guid AggregateId { get; }
    }
}