using System;
using Fibers;
using SGame.UI;
using System.Collections;
using FairyGUI;
using log4net;
using UnityEngine;

namespace SGame.Hotfix
{
    // 热更新UI 脚本, 更新逻辑包含再这这里面
    public class UIHotfix : MonoBehaviour
    {
        private Fiber			m_fiber;
        private GProgressBar	m_progressBar;
        private GTextField		m_text;
        private GTextField		m_ver;
        private EventHanle		m_eventHandle;
        private static ILog log = LogManager.GetLogger("hotfix");
        private GameVersion		m_gameVersion = null;

        private void Start()
        {
	        var uiPanel = GetComponent<UIPanel>();
	        var content = uiPanel.ui;
	        m_progressBar = content.GetChild("Processbar").asProgress;
	        m_text        = content.GetChild("Message").asTextField;
	        m_ver		  = content.GetChild("ver").asTextField;
	        m_fiber       = new Fiber(RunLogic());
	        m_ver.text	  = "";
	        m_eventHandle   = EventManager.Instance.Reg((int)HotfixGameEvent.LOGIN_READLY, OnEventGameLogin);
	        UIUtils.SetLogo(content);
        }
        
        
        void Update()
        {
	        if (m_fiber != null && !m_fiber.IsTerminated)
		        m_fiber.Step();
        }


        void OnEventGameLogin()
        {
            // 关闭UI
            m_eventHandle.Close();
            m_eventHandle = null;
            Destroy(gameObject);
        }

        void RunAppStore()
        {
	        //URL_MARKET_ANDROID			= "https://play.google.com/store/apps/details?id=com.solitaire.patience.klondike.reepl" -- android 应用商店
	        //URL_MARKTET_IOS				= "itms-apps://itunes.apple.com/app/id" 	-- 苹果应用商店
	        var appstore = IniUtils.GetLocalValue("appstore");
	        log.Info("run appstore =" + appstore);
	        UpdateUtils.OpenUrl(appstore);
        }

        static double GetMb(long value)
        {
	        double v = value;
	        return v / 1024 / 1024;
	        //return string.Format("{0}")
        }
        

        string GetDownloadString(long downloadSize, long totalSize)
        {
	        if (totalSize == 0)
		        return "";

	        return string.Format("DOWNLOAD SIZE={0:N2}MB/{1:N2}MB", GetMb(downloadSize), GetMb(totalSize));
        }

        IEnumerator RunLogic()
        {
	        var update_url = IniUtils.GetLocalValue("@update_url");
	        if (string.IsNullOrEmpty(update_url))
	        {
		        log.Error("update_url not found!");
		        yield break;
	        }

	        if (!int.TryParse(IniUtils.GetLocalValue("update_timeout"), out int timeOut))
	        {
		        log.Error("update_timeout not found!");
		        yield break;
	        }
	        
	        log.Info("update_url=" + update_url);
			VersionUpdater.Instance.Initalize(update_url, timeOut);
			VersionUpdater updater = VersionUpdater.Instance;
			m_text.text = "Checking";
			
            /// 测试代码
            m_progressBar.min = 0;
            m_progressBar.max = 100;
            while (updater.isDone == false)
            {
	            // 更新当前版本号的线上
	            if (m_gameVersion == null && updater.state >= VersionUpdater.STATE.CHECK_VERSION)
	            {
		            m_gameVersion = updater.gameVersion;
		            m_ver.text = m_gameVersion.showVer;
	            }
	            
	            var state = updater.state;
	            if (state == VersionUpdater.STATE.DOWNLOADING)
	            {
		            double total = updater.totalSize;
		            total = total > 0 ? total : 1;
		            double proggress = updater.downloadSize / total;
		            m_text.text = GetDownloadString(updater.downloadSize, updater.totalSize);//"DOWNLOAD SIZE =" + updater.downloadSize + " bytes";
		            m_progressBar.value = proggress * 100.0f;
		            log.Info("hotfix download file=" + updater.downloadSize);
	            }
	            yield return null;
            }

            if (updater.errCode == VersionUpdater.Error.APP_STORE)
            {
	            log.Info("hotfix to appstore ....");
	            RunAppStore();
	            yield break;
            }
            
            if (!string.IsNullOrEmpty(updater.error))
            {
	            log.Info("hotfix fail ....");
	            m_text.text = updater.error;
	            yield break;
            }
			
			// 热更新结束, 保存版本号
			GameData.gameVersion = updater.gameVersion;
			EventManager.Instance.Trigger((int)HotfixGameEvent.HOTFIX_DONE);
			log.Info("hotfix successs ....");
        }



	}
}