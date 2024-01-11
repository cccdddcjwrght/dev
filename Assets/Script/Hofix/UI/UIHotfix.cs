using Fibers;
using SGame.UI;
using System.Collections;
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;

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

        static string GetStateString(VersionUpdater.STATE state)
        {
            switch (state)
            {
                case VersionUpdater.STATE.INIT: // 初始化
                    return  "Init...";
                case VersionUpdater.STATE.COPY_STREAM_ASSETS: // 拷贝本地版本信息与MD5
                    return "Copy Stream Assets...";
                case VersionUpdater.STATE.CHECK_VERSION: // 检测版本号
                    return "Copy Version ...";
                case VersionUpdater.STATE.COUNT_UPDATELIST: // 计算下载资源列表
                    return "Count Update List ...";
                case VersionUpdater.STATE.DOWNLOADING: // 下载更新资源
                    return "Down Load Files ..";
                case VersionUpdater.STATE.CHECK_MD5: // 校验文件MD5
                    return "Count Update List ...";
                case VersionUpdater.STATE.COPY_FILES: // 拷贝文件
                    return "Extarct Files ...";
                case VersionUpdater.STATE.RELOAD: // 重新加载资源管理
                    return "Reload ...";
            }

            return "Unknown";
        }

        /// <summary>
        /// 显示下载进度
        /// </summary>
        /// <param name="context"></param>
        void ShowDownloads()
        {
            VersionUpdater updater = VersionUpdater.Instance;
            var totalSize = updater.totalSize;
            var downloadSize = updater.downloadSize;
            if (totalSize == 0)
                totalSize = 1;
            
            var downloadStr = string.Format(
                "download..  {0}MB/{1}MB", math.min(downloadSize / 1024 / 1024, 2), 
                                 math.min(totalSize / 1024 / 1024, 2));

            m_text.text = downloadStr;
        }
        
        /// <summary>
        /// UI 运行更新流程
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        IEnumerator RunLogic(UIContext context)
        {
            VersionUpdater updater = VersionUpdater.Instance;
            updater.Initalize(Define.REMOTE_URL);
            var state       = updater.state;
            var lastState   = VersionUpdater.STATE.INIT;
            while (state < VersionUpdater.STATE.FAIL)
            {
                yield return null;
                state = updater.state;
                
                // 下载暂停了
                if (updater.Pause)
                    continue; 

                bool change = lastState != state;
                if (state == VersionUpdater.STATE.DOWNLOADING)
                switch (state)
                {
                    case VersionUpdater.STATE.DOWNLOADING:          // 显示下载进度
                        ShowDownloads();
                        break;
                    default:
                        if (change)
                            m_text.text = GetStateString(state);
                        break;
                }
            }

            // 热更新结束, 成功就发送事件
            state = updater.state;
            if (state != VersionUpdater.STATE.FAIL)
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