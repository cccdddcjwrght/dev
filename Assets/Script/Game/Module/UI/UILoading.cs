using System.Collections;
using Fibers;
using SGame.UI;


namespace SGame
{
    // 热更新UI脚本, DEMO 阶段暂时不使用, 预留代码用于后续扩展
    public class UILoading : IUIScript
    {
        // 协程对象
        private Fiber m_fiber;
        
        public void OnInit(UIContext context)
        {
            context.onUpdate += onUpdate;
            m_fiber = new Fiber(RunLogic(context));
        }

        IEnumerator RunLogic(UIContext context)
        {
            //  播放动画, 等待3秒结束
            yield return FiberHelper.Wait(3);
            context.window.Close();
        }

        void onUpdate(UIContext context)
        {
            m_fiber.Step();
        }

        public static IUIScript Create()
        {
            return new UILoading();
        }
    }
}