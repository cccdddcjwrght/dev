
using SGame.UI;
namespace SGame
{
    /// <summary>
    /// 用于处理UI的预处理
    /// </summary>
    public class UIPreprocess : IPreprocess
    {
        // 初始化UI状态, 包括是否全屏等等
        public void Init(UIContext context)
        {
            
        }

        public bool GetUIInfo(int configId, out string comName, out string pkgName)
        {
            comName = null;
            pkgName = null;
            if (ConfigSystem.Instance.TryGet(configId, out GameConfigs.ui_resRowData ui))
            {
                comName = ui.ComName;
                pkgName = ui.PackageName;
                return true;
            }

            return false;
        }
    }
}