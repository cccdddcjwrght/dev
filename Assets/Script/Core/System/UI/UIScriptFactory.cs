using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using log4net;

namespace SGame.UI
{
    public class UIScriptFactory : Singleton<UIScriptFactory>
    {
        private Dictionary<UIInfo, CREATER> m_factory;
        public delegate IUIScript CREATER();
        private static ILog log = LogManager.GetLogger("xl.ui");

        public UIScriptFactory()
        {
            m_factory = new Dictionary<UIInfo, CREATER>(32);
        }
        
        public IUIScript Create(UIInfo key)
        {
            if (m_factory.TryGetValue(key, out CREATER creater))
            {
                return creater();
            }
            
            log.Error("UI SCRITP NOT FOUND=" + key.comName + " pkg=" + key.pkgName);
            return null;
        }

        public bool Register(UIInfo key, CREATER creater)
        {
            if (m_factory.TryAdd(key, creater))
                return true;

            log.Error("KEY REPEATE=" + key.comName + " pkg=" + key.pkgName);
            return false;
        }

        public void Dispose()
        {
            m_factory.Clear();
        }
    }
}