
using log4net;
using SGame.UI;
using UnityEngine;
namespace SGame
{
    /// <summary>
    /// 用于处理UI的预处理
    /// </summary>
    public class UIPreprocess : IPreprocess
    {
        private static ILog log = LogManager.GetLogger("xl.ui");
        
        // 初始化UI状态, 包括是否全屏等等
        public void Init(UIContext context)
        {
            if (context.configID != 0)
            {
                if (!ConfigSystem.Instance.TryGet(context.configID, out GameConfigs.ui_resRowData ui))
                {
                    log.Error("ui config not found=" + context.configID);
                    return;
                }

                // 设置UI显示层级
                context.window.sortingOrder = ui.Order;
                context.window.uiname = ui.Name;
                
                // 添加UI对动画 显示与隐藏的支持
                UIAnimationBind.Bind(context);
                //context.doShowAnimation = UIAnimationBind.DoShowAnimation;
                //context.doHideAnimation = UIAnimationBind.DoHideAnimation;
            }
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