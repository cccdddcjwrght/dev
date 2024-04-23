
using log4net;
using SGame.UI;
using UnityEngine;
using Unity.Entities;
namespace SGame.Hotfix
{
    /// <summary>
    /// 热更新 UI的预处理
    /// </summary>
    public class UIPreprocess : IPreprocess
    {
        private static ILog log = LogManager.GetLogger("hotfix");
        
        // 初始化UI状态, 包括是否全屏等等
        public void Init(UIContext context)
        {
            if (context.configID == Define.HOTFIX_UI_ID)
            {
                // 设置UI显示层级
                context.window.sortingOrder = 1000;
                context.window.uiname = Define.HOTFIX_UI_NAME;
				context.window.isFullScreen = true;

			}
		}
        
        public void AfterShow(UIContext context)
        {
            
        }

        public bool GetUIInfo(int configId, out string comName, out string pkgName)
        {
            comName = null;
            pkgName = null;
            if (configId == Define.HOTFIX_UI_ID)
            {
                comName = Define.HOTFIX_UI_COM;
                pkgName = Define.HOTFIX_PACKAGE_NAME;
                return true;
            }

            return false;
        }
    }
}