using Fibers;
using SGame.UI;
using System.Collections;

namespace SGame
{
    // 热更新UI脚本, DEMO 阶段暂时不使用, 预留代码用于后续扩展
    public class UIHotfix : IUIScript
    {
        private Fiber m_fiber;
        private float m_waitTime;
        
        public static IUIScript Create() { return new UIHotfix(); }

        public void OnInit(UIContext context)
        {
            context.onUpdate += onUpdate;
            m_fiber = new Fiber(RunLogic(context));
            
            // 获得参数
            m_waitTime = context.gameWorld.GetEntityManager().GetComponentData<UIParamFloat>(context.entity).Value;
        }
        
        IEnumerator RunLogic(UIContext context)
        {
            // 播放动画
            yield return FiberHelper.Wait(3);
            EventManager.Instance.Trigger((int)GameEvent.HOTFIX_DONE);
        }

        void onUpdate(UIContext context)
        {
            m_fiber.Step();
        }
    }
}