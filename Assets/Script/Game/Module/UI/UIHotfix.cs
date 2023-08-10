using Fibers;
using SGame.UI;
using System.Collections;
using UnityEngine;

namespace SGame
{
    // 热更新UI脚本, DEMO 阶段暂时不使用, 预留代码用于后续扩展
    public class UIHotfix : IUIScript
    {
        private Fiber m_fiber;
        private float m_waitTime;
        private FairyGUI.GProgressBar m_progressBar;
        private FairyGUI.GTextField   m_text;
        private const int MAX_DOWNLOAD = 10;
        
        public static IUIScript Create() { return new UIHotfix(); }

        public void OnInit(UIContext context)
        {
            m_progressBar = context.content.GetChild("Processbar").asProgress;
            m_text        = context.content.GetChild("Message").asTextField;
            context.onUpdate += onUpdate;
            m_fiber = new Fiber(RunLogic(context));
            
            // 获得参数
            m_waitTime = context.gameWorld.GetEntityManager().GetComponentData<UIParamFloat>(context.entity).Value;
        }
        
        IEnumerator RunLogic(UIContext context)
        {
            // 播放动画
            
            yield return FiberHelper.Wait(3);
            float run = 0;
            m_progressBar.min = 0;
            m_progressBar.max = m_waitTime;
            while (run <= m_waitTime)
            {
                run += Time.deltaTime;
                float per = Mathf.Clamp01(run / m_waitTime);
                float size = MAX_DOWNLOAD * per;
                m_text.text = string.Format("DOWNLOAD SIZE={0:0.00}MB/{1}MB", size, MAX_DOWNLOAD);
                m_progressBar.value = run;
                yield return null;
            }
            m_progressBar.value = m_waitTime;

            EventManager.Instance.Trigger((int)GameEvent.HOTFIX_DONE);
        }

        void onUpdate(UIContext context)
        {
            m_fiber.Step();
        }
    }
}