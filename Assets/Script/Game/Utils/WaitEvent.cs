using System.Collections;
using Unity.Entities;

namespace SGame
{
    // 等待事件
    public class WaitEvent : IEnumerator
    {
        private EntityManager     m_mgr;
        private EventHanle        m_eventHandle;
        private bool              m_isTrigger;
        private GameEvent         m_eventId;

        public WaitEvent(EntityManager mgr, GameEvent eventId)
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
    public class WaitEventV1<T> : IEnumerator
    {
        private EntityManager     m_mgr;
        private EventHanle        m_eventHandle;
        private bool              m_isTrigger;
        private GameEvent         m_eventId;
        public T                  m_Value;
        
        public WaitEventV1(EntityManager mgr, GameEvent eventId)
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
}