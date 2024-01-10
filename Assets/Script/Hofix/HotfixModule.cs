using System;
using System.Collections;
using System.Collections.Generic;
using log4net;
using plyLib;
using SGame.UI;
using UnityEngine;
using UnityEngine.Diagnostics;
using SGame;

namespace SGame.Hotfix
{
// 
    public class HotfixModule : SingletonMonoBehaviour<HotfixModule>
    {
        private static ILog log = LogManager.GetLogger("Hofix.Module");

        private GameWorld       m_gameWorld;
        private EventHanle      m_eventHandle;

        void InitUI()
        {
            UIModule.Instance.Initalize(m_gameWorld,new UIPreprocess());
            UIModule.Instance.Reg(Define.HOTFIX_UI_COM, Define.HOTFIX_PACKAGE_NAME, UIHotfix.Create);
        }
        
        public IEnumerator RunHotfix()
        {
            m_eventHandle   = EventManager.Instance.Reg((int)GameEvent.LOGIN_READLY, OnEventGameLogin);
            m_gameWorld     = new GameWorld("hotfix");
            InitUI();

            // 创建UI并等待加载完毕
            var ui = UIRequest.Create(m_gameWorld.GetEntityManager(), Define.HOTFIX_UI_ID);
            while (UIModule.Instance.CheckOpened(ui) == false)
                yield return null;

            // 等待热更新结束
            WaitEvent w = new WaitEvent((int)GameEvent.HOTFIX_DONE);
            yield return w;
        }

        /// <summary>
        /// 登录开始
        /// </summary>
        void OnEventGameLogin()
        {
            m_gameWorld.Shutdown();
            m_gameWorld = null;
            
            m_eventHandle.Close();
            m_eventHandle = null;
            
            // 销毁自己
            Destroy(gameObject);
        }

        private void Update()
        {
            if (m_gameWorld != null)
                m_gameWorld.ProcessDespawns();
        }
    }
}
