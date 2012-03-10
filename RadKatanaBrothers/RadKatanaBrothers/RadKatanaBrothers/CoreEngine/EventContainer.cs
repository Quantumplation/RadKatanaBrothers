using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RadKatanaBrothers
{
    public class EventContainer
    {
        #region Internals
        Dictionary<String, IGameEvent> coEvents = new Dictionary<String, IGameEvent>();

        private interface IGameEvent { }
        private class GameEvent<T> : IGameEvent where T : class
        {
            static GameEvent()
            {
#if DEBUG
                if (!typeof(T).IsSubclassOf(typeof(Delegate)))
                {
                    throw new InvalidOperationException("WARNING: MUST ONLY USE DELEGATE TYPES WITH GameEvent<T>!!!");
                }
#endif
            }

            Delegate coDelegate;
            public T Callback
            {
                get { return coDelegate as T; }
                set { coDelegate = value as Delegate; }
            }

            public void Add(T del)
            {
                coDelegate = Delegate.Combine(coDelegate, del as Delegate);
            }

            public void Remove(T del)
            {
                coDelegate = Delegate.RemoveAll(coDelegate, del as Delegate);
            }
        }

        #endregion

        public bool HasEvent(String pcKey) { return coEvents.ContainsKey(pcKey); }

        public void AddEvent<T>(String poKey, T poEvent) where T : class
        {
            bool contains = coEvents.ContainsKey(poKey);
            GameEvent<T> evt = (contains ? coEvents[poKey] as GameEvent<T> : new GameEvent<T>());
            if (!contains)
                coEvents.Add(poKey, evt);
            evt.Add(poEvent);
        }
        public T GetEvent<T>(String poKey) where T : class
        {
            if (coEvents.ContainsKey(poKey))
                return (coEvents[poKey] as GameEvent<T>).Callback;
            return default(T);
        }
        public void RemoveEvent<T>(String poKey, T poEvent) where T : class
        {
            if (coEvents.ContainsKey(poKey))
                (coEvents[poKey] as GameEvent<T>).Remove(poEvent);
        }
    }
}
