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
        private Fiber                   m_fiber;
        private float                   m_waitTime;
        private FairyGUI.GProgressBar   m_progressBar;
        private FairyGUI.GTextField     m_text;
        private const int               MAX_DOWNLOAD = 10;
        private EventHanle              m_eventHandle;      // 事件
        private Entity                  m_entity;           // UI

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
        }

        /// <summary>
        /// 登录界面已经准备号, 
        /// </summary>
        void OnEventGameLogin()
        {
            // 关闭UI
            UIModule.Instance.CloseUI(m_entity);
            m_eventHandle.Close();
            m_eventHandle = null;
        }
        
        IEnumerator RunLogic(UIContext context)
        {
            VersionUpdater updater = VersionUpdater.Instance;
            updater.Initalize(Define.REMOTE_URL);
            var state = updater.state;
            while (state < VersionUpdater.STATE.FAIL)
            {
                state = updater.state;
                switch (state)
                {
                    case VersionUpdater.STATE.INIT:                 // 初始化
                        break;
                    case VersionUpdater.STATE.COPY_STREAM_ASSETS:   // 拷贝本地版本信息与MD5
                        break;
                    case VersionUpdater.STATE.CHECK_VERSION:        // 检测版本号
                        break;
                    case VersionUpdater.STATE.COUNT_UPDATELIST:     // 计算下载资源列表
                        break;
                    case VersionUpdater.STATE.DOWNLOADING:          // 下载更新资源
                        break;
                    case VersionUpdater.STATE.CHECK_MD5:            // 校验文件MD5
                        break;
                    case VersionUpdater.STATE.COPY_FILES:           // 拷贝文件
                        break;
                    case VersionUpdater.STATE.RELOAD:               // 重新加载资源管理
                        break;
                }
                yield return null;
            }
            
            
            

            // 热更新结束, 发送事件
            EventManager.Instance.Trigger((int)GameEvent.HOTFIX_DONE);
        }

        IEnumerator Test()
        {
            /// 测试代码
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
        }

        void onUpdate(UIContext context)
        {
            if (m_fiber != null && !m_fiber.IsTerminated)
                m_fiber.Step();
        }
    }
}