using System.Collections;
using System.Collections.Generic;
using SGame.UI;
using UnityEngine;
using FairyGUI;
using log4net;
using Unity.Entities;

namespace SGame.UI
{
        /// <summary>
        /// 对象池
        /// </summary>
        public class UIPool
        {
            private static ILog log = LogManager.GetLogger("ui");
            public SObjectPool<FairyWindow> pool;
            public UIPackageRequest        uiPackage;  // UI包
            public string                  comName;    // 原件名称
            public int                     m_cacheNum; // 缓存数量
            private int                    m_configID; 
            public delegate FairyWindow NewOneDelegate(UIPackage pkg, string comName);
            private NewOneDelegate          m_onNewOne;
            
            public UIPool(int configID, UIPackageRequest pkg, string comName, 
                NewOneDelegate onNewOne,
                SObjectPool<FairyWindow>.SpawnDelegate onSpawn, 
                SObjectPool<FairyWindow>.DeSpawnDelegate onDespawn,
                SObjectPool<FairyWindow>.DisposeDelegate onDispose,
                int cacheNum)
            {
                pool = new SObjectPool<FairyWindow>(NewOne, onSpawn, onDespawn, onDispose);
                this.comName = comName;
                this.uiPackage = pkg;
                this.m_configID = configID;
                this.m_onNewOne = onNewOne;
                this.m_cacheNum = cacheNum;
                pkg.Retain();
            }

            public int cacheNum => m_cacheNum;

            UIContext CreateContext(FairyGUI.GComponent content, int configId )
            {
                UIContext context = new UIContext();
                context.gameWorld = null;
                context.uiModule = null;
                context.entity = Entity.Null;
                context.content = content;
                context.configID = configId;
                return context;
            }

            /// <summary>
            /// 创建新的UI
            /// </summary>
            /// <returns></returns>
            FairyWindow NewOne() => m_onNewOne(uiPackage.uiPackage, comName);
            
            /// <summary>
            /// 生成
            /// </summary>
            /// <returns></returns>
            public PoolID Alloc()       => pool.Alloc();
            
            /// <summary>
            /// 释放资源
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
            public bool Free(PoolID id) => pool.Free(id);

            public bool Dispose(PoolID id) => pool.Dispose(id);
            
            public FairyWindow Get(PoolID id)
            {
                if (pool.TryGet(id, out FairyWindow ret))
                    return ret;

                return null;
            }

            public int usedCount => pool.usedCount;

            public int count => pool.allCount;

            public void Dispose()
            {
                uiPackage.Release();
                uiPackage = null;
            }
        }
           
    /// <summary>
    /// FairyWindow 创建工厂
    /// </summary>
    public class UIFactory : Singleton<UIFactory>
    {
        private Dictionary<int, UIPool> m_uiPools  = new Dictionary<int, UIPool>();

        private static ILog log = LogManager.GetLogger("ui");
        
        void OnSpawnUI(FairyWindow window)
        {
            
        }

        void OnDespawnUI(FairyWindow window)
        {
            window.HideImmediately();
        }

        void OnDispose(FairyWindow window)
        {
			window.context?.onUninit?.Invoke(window.context);
            window.Dispose();
        }
        
        FairyWindow NewOne(UIPackage uiPackage, string comName)
        {
            var gObject = uiPackage.CreateObject(comName);
            if (gObject == null)
            {
                log.Error("Package " + uiPackage.name + " ui=" + comName + " not found");
                return null;
            }
                
            // 4. 创建WINDOW
            var fui = new FairyWindow();
            fui.bringToFontOnClick = false; // 点击不会改变顺序
            var gCom = gObject.asCom;
            fui.contentPane = gCom;
            return fui;
        }
        
        UIPool GetOrCreate(int configID, UIPackageRequest uiPackage, string comName, int cacheNum)
        {
            if (m_uiPools.TryGetValue(configID, out UIPool ret))
            {
                return ret;
            }

            ret = new UIPool(configID,
                uiPackage, 
                comName, 
                NewOne,
                OnSpawnUI, 
                OnDespawnUI,
                OnDispose,
                cacheNum);
            m_uiPools.Add(configID, ret);
            return ret;
        }

        // 分配新的UI
        /// <summary>
        /// cacheNum 缓存数量
        /// </summary>
        /// <param name="configID"></param>
        /// <param name="uiPackage"></param>
        /// <param name="comName"></param>
        /// <param name="cacheNum"></param>
        /// <returns></returns>
        public FairyWindow Alloc(int configID, UIPackageRequest uiPackage, string comName, int cacheNum)
        {
            // 4. 创建WINDOW
            if (cacheNum == 0)
            {
                return NewOne(uiPackage.uiPackage, comName);
            }
            
            var pool = GetOrCreate(configID, uiPackage, comName, cacheNum);
            var pid = pool.Alloc();
            var ret = pool.Get(pid);
            ret.poolID = pid;
            return ret;
        }

        /// <summary>
        /// 释放UI缓存
        /// </summary>
        /// <param name="window"></param>
        /// <returns></returns>
        public bool Free(FairyWindow window)
        {
            var id = window.configID;
            var poolID = window.poolID;
            if (!m_uiPools.TryGetValue(id, out UIPool pool))
            {
                OnDispose(window);
                return true;
            }

            if (pool.count > pool.cacheNum)
            {
                // 超出缓存数量直接销毁
                return pool.Dispose(poolID);
            }
            return pool.Free(poolID);
        }

        /// <summary>
        /// 获得缓存池总数
        /// </summary>
        /// <param name="configID"></param>
        /// <returns></returns>
        public int GetCount(int configID)
        {
            if (!m_uiPools.TryGetValue(configID, out UIPool pool))
            {
                return 0;
            }

            return pool.count;
        }

        /// <summary>
        /// 销毁UI
        /// </summary>
        /// <param name="window"></param>
        public bool Dispose(FairyWindow window)
        {
            var id = window.configID;
            var poolID = window.poolID;
            if (!m_uiPools.TryGetValue(id, out UIPool pool))
            {
                return false;
            }

            return pool.Dispose(poolID);
        }
    }
}