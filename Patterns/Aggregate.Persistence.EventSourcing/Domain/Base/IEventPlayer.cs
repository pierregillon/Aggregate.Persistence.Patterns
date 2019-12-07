using System.Collections.Generic;

namespace Aggregate.Persistence.EventSourcing.Domain.Base
{
    public interface IEventPlayer
    {
        void Replay(IEnumerable<IDomainEvent> events);
    }
}