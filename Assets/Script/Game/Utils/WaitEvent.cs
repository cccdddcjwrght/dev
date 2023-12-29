using System.Collections;
using Unity.Entities;

namespace SGame
{
    // 等待事件
    public class WaitEvent : IEnumerator
    {
        private EventHanle        m_eventHandle;
        private bool              m_isTrigger;
        private GameEvent         m_eventId;

        public WaitEvent(GameEvent eventId)
        {
            m_eventId = eventId;
            Reset();
        }

        public bool MoveNext()
        {
            return !m_isTrigger;
        }
        
        public object Current { get { return null; } }

        public void Reset()
        {
            m_isTrigger = false;
            if (m_eventHandle != null)
                m_eventHandle.Close();

            m_eventHandle = EventManager.Instance.Reg((int)m_eventId, OnEventTrigger);
        }

        void OnEventTrigger()
        {
            m_isTrigger = true;
            m_eventHandle.Close();
        }
    }
    
    // 带一个参数的等待事件
    public class WaitEvent<T> : IEnumerator
    {
        private EventHanle        m_eventHandle;
        private bool              m_isTrigger;
        private GameEvent         m_eventId;
        public T                  m_Value;
        
        public WaitEvent(GameEvent eventId)
        {
            m_eventId = eventId;
            Reset();
        }

        public bool MoveNext()
        {
            return !m_isTrigger;
        }
        
        public object Current { get { return null; } }

        public void Reset()
        {
            m_isTrigger = false;
            if (m_eventHandle != null)
                m_eventHandle.Close();

            m_eventHandle = EventManager.Instance.Reg<T>((int)m_eventId, OnEventTrigger);
        }

        void OnEventTrigger(T value)
        {
            m_isTrigger = true;
            m_Value = value;
            m_eventHandle.Close();
        }
    }
    
    // 带一个参数的等待事件
    public class WaitEvent<T1,T2> : IEnumerator
    {
        private EventHanle        m_eventHandle;
        private bool              m_isTrigger;
        private GameEvent         m_eventId;
        public T1                  m_Value1;
        public T2                  m_Value2;

        public WaitEvent(GameEvent eventId)
        {
            m_eventId = eventId;
            Reset();
        }

        public bool MoveNext()
        {
            return !m_isTrigger;
        }
        
        public object Current { get { return null; } }

        public void Reset()
        {
            m_isTrigger = false;
            if (m_eventHandle != null)
                m_eventHandle.Close();

            m_eventHandle = EventManager.Instance.Reg<T1, T2>((int)m_eventId, OnEventTrigger);
        }

        void OnEventTrigger(T1 value1, T2 value2)
        {
            m_isTrigger = true;
            m_Value1 = value1;
            m_Value2 = value2;
            m_eventHandle.Close();
        }
    }
}