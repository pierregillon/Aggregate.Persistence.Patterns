using System;
using System.Collections.Generic;

namespace Patterns.EventSourcing.Domain.Events
{
    public abstract class EventOwner
    {
        private readonly IDictionary<Type, Delegate> _eventCallbacks = new Dictionary<Type, Delegate>();
        private readonly IList<IDomainEvent> _uncommittedEvents = new List<IDomainEvent>();

        public void Replay(IEnumerable<IDomainEvent> events)
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
            _eventCallbacks.Add(typeof (TEvent), callback);
        }
        protected void Apply<TEvent>(TEvent @event) where TEvent : IDomainEvent
        {
            Delegate callback;
            if (_eventCallbacks.TryGetValue(typeof (TEvent), out callback)) {
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