
// 用于在Fiber 对象池里面 安全关闭FIBER
namespace Fibers
{
    public struct FiberHandle 
    {
        private Fibers.Fiber m_fiber;
        private int			 m_version;

        public FiberHandle(Fiber fiber)
        {
            m_version = fiber.version;
            m_fiber = fiber;
        }
		
        public void Close()
        {
            if (m_fiber != null)
            {
                if (m_version == m_fiber.version)
                {
                    if (!m_fiber.IsTerminated)
                        m_fiber.Terminate();
                }

                m_fiber = null;
                m_version = 0;
            }
        }
    }
}