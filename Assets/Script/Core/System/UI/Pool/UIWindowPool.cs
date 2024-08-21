using FairyGUI;
using log4net;
using Unity.Entities;


namespace SGame.UI
{
    /// <summary>
    /// 对象池
    /// </summary>
    public class UIWindowPool
    {
        private static ILog log = LogManager.GetLogger("ui");
        public SObjectPool<FairyWindow> pool;
        public UIPackageRequest uiPackage; // UI包
        public string comName;             // 原件名称
        public int m_cacheNum;             // 缓存数量
        private int m_configID;
        public delegate FairyWindow NewOneDelegate(UIPackage pkg, string comName);

        public UIWindowPool(int configID, 
            UIPackageRequest pkg, 
            string comName,
            int cacheNum)
        {
            pool = new SObjectPool<FairyWindow>(NewOne, OnSpawnUI, OnDespawnUI, OnDispose);
            this.comName = comName;
            this.uiPackage = pkg;
            this.m_configID = configID;
            this.m_cacheNum = cacheNum;
            pkg.Retain();
        }

        public int cacheNum => m_cacheNum;

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
        
        void OnSpawnUI(FairyWindow window)
        {
            
        }

        void OnDespawnUI(FairyWindow window)
        {
            window.HideImmediately();
        }

        void OnDispose(FairyWindow window)
        {
            Dispose(window);
        }

        FairyWindow NewOne() => NewOne(uiPackage.uiPackage, comName);
        
        static public FairyWindow NewOne(UIPackage uiPackage, string comName)
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


        public static void Dispose(FairyWindow w)
        {
            if (!w.isDisposed)
            {
                w.context?.onUninit?.Invoke(w.context);
                w.Dispose();
            }
        }

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
}