using System.Collections.Generic;

namespace Patterns.EventSourcing.Domain.Base
{
    public interface IEventPlayer
    {
        void Replay(IEnumerable<IDomainEvent> events);
    }
}