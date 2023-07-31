using System;


namespace SGame
{
    public class RequestAction : IRequest
    {
        private Action  m_action;
        private bool    m_isRun;
        
        public RequestAction(Action action)
        {
            m_action = action;
            m_isRun = false;
        }
        
        // 是否存在错误
        public string error
        {
            get { return null; }
        }
    
        // 请求是否结束
        public bool isDone
        {
            get
            {
                if (m_isRun)
                    return true;

                if (m_action != null)
                    m_action();
                m_isRun = true;
                return true;
            }
        }

        // 释放请求 (非必要）
        public void Close()
        {
            m_action = null;
        }
    }
}