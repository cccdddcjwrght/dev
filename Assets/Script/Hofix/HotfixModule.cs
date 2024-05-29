using System;
using System.Collections;
using System.Collections.Generic;
using libx;
using log4net;
using plyLib;
using SGame.UI;
using UnityEngine;
using UnityEngine.Diagnostics;
using SGame;

namespace SGame.Hotfix
{
// 
    public class HotfixModule : Singleton<HotfixModule>
    {
        private static ILog log = LogManager.GetLogger("Hofix.Module");

        private EventHanle                       m_eventHandle;
        private ResourceLoader<UIPackageRequest> m_packageRequest;
        
        // PACKAGE 工厂
        UIPackageRequest PackageRequestFactory()
        {
            var ret = new UIPackageRequest((uiPackage) =>
            {
                 log.Warn("load package =" + uiPackage);
                 return m_packageRequest.Load(uiPackage);
            });
            return ret;
        }
        
        // 判断是否加载完成
        IEnumerator UIPackageReadly(UIPackageRequest req)
        {
            while (req.isDone == false)
            {
                m_packageRequest.Update();
                yield return null;
            }
            m_packageRequest.Update();
        }

        public IEnumerator RunHotfix()
        {
            if (!UpdateUtils.HasNetwork())
            {
                log.Error("no network, update cancle!");
                yield break;
            }
            
            // UI包加载
            m_packageRequest         = new ResourceLoader<UIPackageRequest>(PackageRequestFactory);
            UIPackageRequest uiPkg  = m_packageRequest.Load(Define.HOTFIX_PACKAGE_NAME);

            // 预制加载
            var loadPrefab = Assets.LoadAssetAsync(Define.HOTFIX_UI_PREFAB, typeof(GameObject));

            // 等待资源加载完成
            yield return FiberHelper.RunParallel(UIPackageReadly(uiPkg), loadPrefab);

            var uiObject = GameObject.Instantiate(loadPrefab.asset as GameObject);

            // 等待热更新结束
            WaitEvent w = new WaitEvent((int)HotfixGameEvent.HOTFIX_DONE);
            yield return w;
        }
    }
}
