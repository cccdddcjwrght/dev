using Unity.Entities;

namespace SGame.UI
{
    /// <summary>
    /// UI 系统预处理, 每个项目的UI系统的配置表都不一样, 预处理也不一样
    /// </summary>
    public interface IPreprocess
    {
        void Init(UIContext context);

        void ReOpen(UIContext context);

        void AfterShow(UIContext context);

        bool GetUIInfo(int configId, out string comName, out string pkgName);
        
        // 缓存数量
        int GetCacheNum(int configID);
    }
}