
using SGame.UI;
namespace SGame
{
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
                comName = ui.Name;
                pkgName = ui.PackageName;
                return true;
            }

            return false;
        }
    }
}