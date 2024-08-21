using FairyGUI;
using log4net;
using Unity.Entities;


namespace SGame.UI
{
    /// <summary>
    /// 子UI对象池
    /// </summary>
    public class UIComWindowPool
    {
        private static ILog log = LogManager.GetLogger("ui");
        public SObjectPool<ComWindow> pool;
        public UIPackageRequest uiPackage; // UI包
        public string comName;             // 原件名称
        public int m_cacheNum;             // 缓存数量
        private int m_configID;
        public delegate ComWindow NewOneDelegate(UIPackage pkg, string comName);

        public UIComWindowPool(
            int configID, 
            UIPackageRequest pkg, 
            string comName,
            int cacheNum)
        {
            pool = new SObjectPool<ComWindow>(NewOne, OnSpawn, OnDespawn, OnDispose);
            this.comName = comName;
            this.uiPackage = pkg;
            this.m_configID = configID;
            this.m_cacheNum = cacheNum;
            pkg.Retain();
        }

        public int cacheNum => m_cacheNum;


        void OnSpawn(ComWindow w)
        {
            
        }

        void OnDespawn(ComWindow w)
        {
            
        }

        void OnDispose(ComWindow w)
        {
            Dispose(w);
        }


        /// <summary>
        /// 创建新的UI
        /// </summary>
        /// <returns></returns>
        ComWindow NewOne() => NewOne(uiPackage.uiPackage, comName);

        public static ComWindow NewOne(UIPackage uiPackage, string comName)
        {
            var gObject = uiPackage.CreateObject(comName) as GComponent;
            if (gObject == null)
            {
                log.Error("Package " + uiPackage.name + " ui=" + comName + " not found");
                return null;
            }

            ComWindow win = new ComWindow(gObject);
            return win;
        }

        /// <summary>
        /// 生成
        /// </summary>
        /// <returns></returns>
        public PoolID Alloc() => pool.Alloc();

        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Free(PoolID id) => pool.Free(id);

        public bool Dispose(PoolID id) => pool.Dispose(id);

        public static void Dispose(ComWindow window)
        {
            window.Dispose();
        }

        public ComWindow Get(PoolID id)
        {
            if (pool.TryGet(id, out ComWindow ret))
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
}