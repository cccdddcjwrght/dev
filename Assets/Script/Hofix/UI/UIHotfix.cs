using Fibers;
using SGame.UI;
using System.Collections;
using Unity.Entities;
using UnityEngine;

namespace SGame.Hotfix
{
    // 热更新UI 脚本, 更新逻辑包含再这这里面
    public class UIHotfix : IUIScript
    {
        private Fiber m_fiber;
        private float m_waitTime;
        private FairyGUI.GProgressBar m_progressBar;
        private FairyGUI.GTextField   m_text;
        private const int MAX_DOWNLOAD = 10;
        private EventHanle m_eventHandle;
        private Entity     m_entity;

        /// <summary>
        /// 测试时间
        /// </summary>
        public const float TEST_TIME = 1.0f;
        
        public static IUIScript Create() { return new UIHotfix(); }

        public void OnInit(UIContext context)
        {
            m_entity = context.entity;
            m_progressBar = context.content.GetChild("Processbar").asProgress;
            m_text        = context.content.GetChild("Message").asTextField;
            context.onUpdate += onUpdate;
            m_fiber       = new Fiber(RunLogic(context));
            
            m_eventHandle   = EventManager.Instance.Reg((int)GameEvent.LOGIN_READLY, OnEventGameLogin);

            
            // 获得参数
            m_waitTime = TEST_TIME;

			UIUtils.SetLogo(context.window.contentPane);

        }

        void OnEventGameLogin()
        {
            // 关闭UI
            UIModule.Instance.CloseUI(m_entity);
            m_eventHandle.Close();
            m_eventHandle = null;
        }
        
        IEnumerator RunLogic(UIContext context)
        {
			//VersionUpdater.Instance.Initalize(Define.REMOTE_URL);

			m_text.text = "Checking";
			// 播放动画
			/*
            /// 测试代码
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
            m_progressBar.value = m_waitTime;*/
			yield return null;
			// 热更新结束, 发送事件
			EventManager.Instance.Trigger((int)GameEvent.HOTFIX_DONE);
        }

        void onUpdate(UIContext context)
        {
            if (m_fiber != null && !m_fiber.IsTerminated)
                m_fiber.Step();
        }

	}
}