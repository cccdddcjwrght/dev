using System.Collections;
using System.Collections.Generic;
using log4net;
using UnityEngine;

namespace SGame.Hotfix
{
    /// <summary>
    /// 热更新模块内地的 常量定义
    /// </summary>
    public class Define
    {
        /// <summary>
        /// 更新代码无法使用配置表, 因此将UI配置信息写入代码中 
        /// </summary>
        public const int        HOTFIX_UI_ID        = 1;        // UI ID
        public const string     HOTFIX_UI_NAME      = "Hotfix"; // UI 名称
        public const string     HOTFIX_UI_COM       = "Hotfix"; // UI 元件名
        public const string     HOTFIX_PACKAGE_NAME = "Hotfix"; // UI 包名

        public const string     REMOTE_URL = "";
    }
}