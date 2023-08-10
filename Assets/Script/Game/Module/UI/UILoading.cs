using System.Collections;
using FairyGUI;
using Fibers;
using SGame.UI;
using Unity.Entities;
using UnityEngine;

namespace SGame
{
    // 热更新UI脚本, DEMO 阶段暂时不使用, 预留代码用于后续扩展
    public class UILoading : IUIScript
    {
        // 协程对象
        private Fiber m_fiber;

        // 显示时间
        private float m_waitTime;

        private GProgressBar m_progressBar;

        private GTextField m_text;
        
        public void OnInit(UIContext context)
        {
            m_progressBar = context.content.GetChild("n3").asProgress;
            m_text = context.content.GetChild("n4").asTextField;
            context.onUpdate += onUpdate;
            m_fiber = new Fiber(RunLogic(context));
            
            // 获得参数
            m_waitTime = context.gameWorld.GetEntityManager().GetComponentData<UIParamFloat>(context.entity).Value;
        }

        IEnumerator RunLogic(UIContext context)
        {
            // 播放动画
            float run = 0;
            m_progressBar.min = 0;
            m_progressBar.max = m_waitTime;
            while (run <= m_waitTime)
            {
                run += Time.deltaTime;
                float per = Mathf.Clamp01(run / m_waitTime);
                m_text.text = string.Format("LOADING ... {0:0.00}%", per * 100);
                m_progressBar.value = run;
                yield return null;
            }
            m_progressBar.value = m_waitTime;

            EventManager.Instance.Trigger((int)GameEvent.ENTER_GAME);
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