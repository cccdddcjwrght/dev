/*
    Event Manager
* 
*/

using System;
using System.Collections.Generic;

namespace SGame
{
    public class EventDispatcher
    {
        public Dictionary<int, Delegate> eventTable = new Dictionary<int, Delegate>();

        public EventDispatcher()
        {
        }

        // Add event listener
        public bool addEventListener(int eventType, Callback function)
        {
            if (!recordEvent(eventType, function)) return false;
            eventTable[eventType] = Delegate.Combine((Callback)eventTable[eventType], function);
            return true;
        }

        public bool addEventListener<T>(int eventType, Callback<T> function)
        {
            if (!recordEvent(eventType, function)) return false;
            eventTable[eventType] = Delegate.Combine((Callback<T>)eventTable[eventType], function);
            return true;
        }

        public bool addEventListener<T, U>(int eventType, Callback<T, U> function)
        {
            if (!recordEvent(eventType, function)) return false;
            eventTable[eventType] = Delegate.Combine((Callback<T, U>)eventTable[eventType], function);
            return true;
        }

        public bool addEventListener<T, U, V>(int eventType, Callback<T, U, V> function)
        {
            if (!recordEvent(eventType, function)) return false;
            eventTable[eventType] = Delegate.Combine((Callback<T, U, V>)eventTable[eventType], function);
            return true;
        }

        public bool addEventListener<T, U, V, P4>(int eventType, Callback<T, U, V, P4> function)
        {
            if (!recordEvent(eventType, function)) return false;
            eventTable[eventType] = Delegate.Combine((Callback<T, U, V, P4>)eventTable[eventType], function);
            return true;
        }


        public bool addEventListener<T, U, V, P4, P5>(int eventType, Callback<T, U, V, P4, P5> function)
        {
            if (!recordEvent(eventType, function)) return false;
            eventTable[eventType] = Delegate.Combine((Callback<T, U, V, P4, P5>)eventTable[eventType], function);
            return true;
        }


        // Remove event listener
        public void removeEventListener(int eventType, Callback function)
        {
            if (!removeEvent(eventType, function)) return;
            eventTable[eventType] = (Callback)Delegate.Remove((Callback)eventTable[eventType], function);
            removeType(eventType);
        }

        public void removeEventListener<T>(int eventType, Callback<T> function)
        {
            if (!removeEvent(eventType, function)) return;
            eventTable[eventType] = (Callback<T>)Delegate.Remove((Callback<T>)eventTable[eventType], function);
            removeType(eventType);
        }

        public void removeEventListener<T, U>(int eventType, Callback<T, U> function)
        {
            if (!removeEvent(eventType, function)) return;
            eventTable[eventType] = (Callback<T, U>)Delegate.Remove((Callback<T, U>)eventTable[eventType], function);
            removeType(eventType);
        }

        public void removeEventListener<T, U, V>(int eventType, Callback<T, U, V> function)
        {
            if (!removeEvent(eventType, function)) return;
            eventTable[eventType] = (Callback<T, U, V>)Delegate.Remove((Callback<T, U, V>)eventTable[eventType], function);
            removeType(eventType);
        }

        public void removeEventListener<T, U, V, P4>(int eventType, Callback<T, U, V, P4> function)
        {
            if (!removeEvent(eventType, function)) return;
            eventTable[eventType] = (Callback<T, U, V, P4>)Delegate.Remove((Callback<T, U, V, P4>)eventTable[eventType], function);
            removeType(eventType);
        }

        public void removeEventListener<T, U, V, P4, P5>(int eventType, Callback<T, U, V, P4, P5> function)
        {
            if (!removeEvent(eventType, function)) return;
            eventTable[eventType] = (Callback<T, U, V, P4, P5>)Delegate.Remove((Callback<T, U, V, P4, P5>)eventTable[eventType], function);
            removeType(eventType);
        }

        public bool IsContains(int eventType)
        {
            return eventTable.ContainsKey(eventType);
        }

        // Dispatch an event
        public void dispatchEvent(int eventType)
        {
            if (!eventTable.ContainsKey(eventType)) return;
            Delegate function;
            if (eventTable.TryGetValue(eventType, out function))
            {
                Callback CallBack = function as Callback;
                if (CallBack != null)
                {
                    CallBack();
                }
            }
        }


        public void dispatchEvent<T>(int eventType, T arg)
        {
            if (!eventTable.ContainsKey(eventType)) return;
            Delegate function;
            if (eventTable.TryGetValue(eventType, out function))
            {
                Callback<T> CallBack = function as Callback<T>;
                if (CallBack != null)
                {
                    CallBack(arg);
                }
            }
        }

        public void dispatchEvent<T, U>(int eventType, T arg1, U arg2)
        {
            if (!eventTable.ContainsKey(eventType)) return;
            Delegate function;
            if (eventTable.TryGetValue(eventType, out function))
            {
                Callback<T, U> CallBack = function as Callback<T, U>;
                if (CallBack != null)
                {
                    CallBack(arg1, arg2);
                }
            }
        }

        public void dispatchEvent<T, U, V>(int eventType, T arg1, U arg2, V arg3)
        {
            if (!eventTable.ContainsKey(eventType)) return;
            Delegate function;
            if (eventTable.TryGetValue(eventType, out function))
            {
                Callback<T, U, V> CallBack = function as Callback<T, U, V>;
                if (CallBack != null)
                {
                    CallBack(arg1, arg2, arg3);
                }
            }
        }

        public void dispatchEvent<T, U, V, P4>(int eventType, T arg1, U arg2, V arg3, P4 arg4)
        {
            if (!eventTable.ContainsKey(eventType)) return;
            Delegate function;
            if (eventTable.TryGetValue(eventType, out function))
            {
                Callback<T, U, V, P4> CallBack = function as Callback<T, U, V, P4>;
                if (CallBack != null)
                {
                    CallBack(arg1, arg2, arg3, arg4);
                }
            }
        }

        public void dispatchEvent<T, U, V, P4, P5>(int eventType, T arg1, U arg2, V arg3, P4 arg4, P5 arg5)
        {
            if (!eventTable.ContainsKey(eventType)) return;
            Delegate function;
            if (eventTable.TryGetValue(eventType, out function))
            {
                Callback<T, U, V, P4, P5> CallBack = function as Callback<T, U, V, P4, P5>;
                if (CallBack != null)
                {
                    CallBack(arg1, arg2, arg3, arg4, arg5);
                }
            }
        }



        // record event, if it doesn't already exists
        private bool recordEvent(int eventType, Delegate function)
        {
            if (!eventTable.ContainsKey(eventType))
            {
                eventTable.Add(eventType, null);
            }
            Delegate d = eventTable[eventType];
            if (d != null && d.GetType() != function.GetType())
            {
                return false;
            }
            return true;
        }

        private bool removeEvent(int eventType, Delegate function)
        {
            if (eventTable.ContainsKey(eventType))
            {
                Delegate d = eventTable[eventType];

                if (d == null)
                {
                    return false;
                }
                else if (d.GetType() != function.GetType())
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        private void removeType(int eventType)
        {
            if (this.eventTable.ContainsKey(eventType) && (this.eventTable[eventType] == null))
            {
                this.eventTable.Remove(eventType);
            }
        }

        public void clear()
        {
            eventTable.Clear();
        }
    }

    public class EventDispatcher_S<T> : EventDispatcher where T : class, new()
    {
        private static T instance = default(T);
        private static readonly object lockHelper = new object();

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockHelper)
                    {
                        if (instance == null)
                        {
                            instance = new T();
                        }
                    }
                }
                return instance;
            }
        }

        public virtual void Initialize() { }

        public virtual void UnInitialize() { }
    }
}