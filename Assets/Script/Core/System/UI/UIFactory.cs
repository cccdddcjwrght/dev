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
    /// FairyWindow 创建工厂
    /// </summary>
    public class UIFactory : Singleton<UIFactory>
    {
        // 带WINDOW 的对象池
        private Dictionary<int, UIWindowPool>       m_uiPools  = new Dictionary<int, UIWindowPool>();
        
        // 不带对象的对象池
        private Dictionary<int, UIComWindowPool> m_comPools = new Dictionary<int, UIComWindowPool>();

        private static ILog log = LogManager.GetLogger("ui");

        UIWindowPool GetOrCreate(int configID, UIPackageRequest uiPackage, string comName, int cacheNum)
        {
            if (m_uiPools.TryGetValue(configID, out UIWindowPool ret))
            {
                return ret;
            }

            ret = new UIWindowPool(configID,
                uiPackage, 
                comName,
                cacheNum);
            m_uiPools.Add(configID, ret);
            return ret;
        }
        
        UIComWindowPool GetOrCreateCom(int configID, UIPackageRequest uiPackage, string comName, int cacheNum)
        {
            if (m_comPools.TryGetValue(configID, out UIComWindowPool ret))
            {
                return ret;
            }

            ret = new UIComWindowPool(configID,
                uiPackage, 
                comName,
                cacheNum);
            m_comPools.Add(configID, ret);
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
        public IBaseWindow Alloc(UI_TYPE type, GComponent parent, int configID, UIPackageRequest uiPackage, string comName, int cacheNum)
        {
            switch (type)
            {
                case UI_TYPE.WINDOW:
                    {
                        if (cacheNum == 0)
                        {
                            return UIWindowPool.NewOne(uiPackage.uiPackage, comName);
                        }

                        var pool = GetOrCreate(configID, uiPackage, comName, cacheNum);
                        var pid = pool.Alloc();
                        var ret = pool.Get(pid);
                        ret.SetPoolID(pid);
                        return ret;
                    }
                case UI_TYPE.COM_WINDOW:
                    {
                        ComWindow ret = null;
                        if (cacheNum == 0)
                        {
                            ret = UIComWindowPool.NewOne(configID, uiPackage.uiPackage, comName);
                        }
                        else
                        {
                            var pool = GetOrCreateCom(configID, uiPackage, comName, cacheNum);
                            var pid = pool.Alloc();
                            ret = pool.Get(pid);
                            ret.SetPoolID(pid);
                        }

                        ret.SetParent(parent);
                        return ret;
                    }
            }
            return null;
        }

        /// <summary>
        /// 释放UI缓存
        /// </summary>
        /// <param name="window"></param>
        /// <returns></returns>
        public bool Free(IBaseWindow window)
        {
            var id = window.configID;
            var poolID = window.poolID;
            var t = window.type;

            // 带WINDOW的UI
            if (t == UI_TYPE.WINDOW)
            {
                if (!m_uiPools.TryGetValue(id, out UIWindowPool pool))
                {
                    UIWindowPool.Dispose(window as FairyWindow);
                    return true;
                }

                if (pool.count > pool.cacheNum)
                {
                    // 超出缓存数量直接销毁
                    return pool.Dispose(poolID);
                }
                var win = pool.Get(poolID);
                if (win.isDisposed)
                    return pool.Dispose(poolID);
                
                return pool.Free(poolID);
            }
            // 基础的UI
            else if (t == UI_TYPE.COM_WINDOW)
            {
                if (!m_comPools.TryGetValue(id, out UIComWindowPool pool))
                {
                    UIComWindowPool.Dispose(window as ComWindow);
                    return true;
                }

                if (pool.count > pool.cacheNum)
                {
                    return pool.Dispose(poolID);
                }

                var win = pool.Get(poolID);
                if (win.isDisposed)
                    return pool.Dispose(poolID);
                
                return pool.Free(poolID);
            }

            return false;
        }

        /// <summary>
        /// 销毁UI
        /// </summary>
        /// <param name="window"></param>
        public bool Dispose(IBaseWindow window)
        {
            var id = window.configID;
            var poolID = window.poolID;
            if (!m_uiPools.TryGetValue(id, out UIWindowPool pool))
            {
                return false;
            }

            return pool.Dispose(poolID);
        }

        /// <summary>
        /// 清空所有缓存
        /// </summary>
        public void ClearCache()
        {
            foreach (var pool in m_uiPools.Values)
            {
                pool.ClearCache();
            }
        }
    }
}