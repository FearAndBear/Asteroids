using System;
using System.Collections.Generic;
using System.Linq;

namespace Asteroids.Events
{
    public static class EventBus
    {
        private static readonly Dictionary<Type, List<EventBase>> Events = new Dictionary<Type, List<EventBase>>();

        public static void Subscribe<T>(Action<T> action)
        {
            var targetType = typeof(T);
            if (!Events.ContainsKey(targetType))
            {
                Events.Add(targetType, new List<EventBase>());
            }
            
            Events[targetType].Add(new Event<T>(action));
        }

        public static void Unsubscribe<T>(Action<T> action)
        {
            var targetType = typeof(T);
            if (Events.ContainsKey(targetType))
            {
                var result = Events[targetType].SingleOrDefault(x =>
                {
                    if (x is Event<T> @event)
                    {
                        return @event.Delegate?.Equals(action) ?? false;
                    }

                    return false;
                });

                if (result != null)
                    Events[targetType].Remove(result);
            }
        }

        public static void Invoke<T>(T obj)
        {
            var targetType = typeof(T);
            if (Events.ContainsKey(targetType))
            {
                int subscribersCount = Events[targetType].Count;
                for (var i = 0; i < Events[targetType].Count; i++)
                {
                    // If subscribers were unsubscribed.
                    int actualCount = Events[targetType].Count;
                    if (subscribersCount > actualCount)
                    {
                        int delta = subscribersCount - actualCount;
                        i -= delta;
                        subscribersCount = actualCount;
                    }

                    var @event = Events[targetType][i] as Event<T>;
                    @event?.Invoke(obj);
                }
            }
        }

        private abstract class EventBase { }
        private class Event<T> : EventBase
        {
            public Event(Action<T> @delegate)
            {
                Delegate = @delegate;
            }

            public Action<T> Delegate { get; }

            public void Invoke(T obj)
            {
                Delegate.Invoke(obj);
            }
        }
    }
}