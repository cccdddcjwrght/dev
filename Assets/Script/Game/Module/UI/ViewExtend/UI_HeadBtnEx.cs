using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame.UI.Common
{
    using GameConfigs;
    using log4net;
    public partial class UI_HeadBtn
    {
        private static ILog log = LogManager.GetLogger("game.ui");
        
        private const string HEAD_ICON_FMT = "ui://IconHead/{0}";
        /// <summary>
        /// 设置UI头像框
        /// </summary>
        /// <param name="head"></param>
        /// <param name="icon"></param>
        /// <param name="frame"></param>
        public void SetHeadIcon(int headID, int frameID)
        {
            /// 设置头像
            if (headID != 0 && ConfigSystem.Instance.TryGet(headID, out AvatarRowData iconConfig))
            {
                if (iconConfig.Type != 1)
                {
                    log.Error("head icon type not match=" + headID);
                    return;
                }

                m_headImg.url = string.Format(HEAD_ICON_FMT, iconConfig.Icon);
            }

            /// 设置头像框
            if (frameID != 0 && ConfigSystem.Instance.TryGet(frameID, out AvatarRowData frameConfig))
            {
                if (frameConfig.Type != 2)
                {
                    log.Error("head frame type not match=" + frameID);
                    return;
                }
                
                m_frame.url	= string.Format(HEAD_ICON_FMT,frameConfig.Icon);
            }
        }
    }
}
