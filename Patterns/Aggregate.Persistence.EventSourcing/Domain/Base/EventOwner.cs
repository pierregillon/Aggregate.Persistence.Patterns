using System;
using System.Collections.Generic;

namespace Patterns.EventSourcing.Domain.Base
{
    public abstract class EventOwner : IEventPlayer
    {
        private readonly IDictionary<Type, Delegate> _eventCallbacks = new Dictionary<Type, Delegate>();
        private readonly IList<IDomainEvent> _uncommittedEvents = new List<IDomainEvent>();

        void IEventPlayer.Replay(IEnumerable<IDomainEvent> events)
        {
            foreach (var domainEvent in events) {
                Apply((dynamic) domainEvent);
            }

            SetEventsAsCommitted();
        }

        public IEnumerable<IDomainEvent> GetUncommittedEvents()
        {
            return _uncommittedEvents;
        }

        protected void RegisterEvent<TEvent>(Action<TEvent> callback) where TEvent : IDomainEvent
        {
            _eventCallbacks.Add(typeof(TEvent), callback);
        }

        protected void Apply<TEvent>(TEvent @event) where TEvent : IDomainEvent
        {
            if (_eventCallbacks.TryGetValue(typeof(TEvent), out var callback)) {
                callback.DynamicInvoke(@event);
            }

            _uncommittedEvents.Add(@event);
        }

        private void SetEventsAsCommitted()
        {
            _uncommittedEvents.Clear();
        }
    }
}