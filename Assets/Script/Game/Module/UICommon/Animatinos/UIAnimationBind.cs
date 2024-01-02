using System.Collections;
using System.Collections.Generic;
using SGame.UI;
using UnityEngine;

namespace SGame.UI
{
    public class UIAnimationBind
    {
        /// <summary>
        /// 动画效果类型
        /// </summary>
        public enum ANIM_TYPE
        {
            INTER = 0, // 内置动画效果
            SCALE = 1, // 缩放动画效果
        }

        static ANIM_TYPE GetAnimType(int anim)
        {
            if (anim == 0)
                return ANIM_TYPE.INTER;
            
            return ANIM_TYPE.SCALE;
        }

        public static void Bind(UIContext context)
        {
            if (!ConfigSystem.Instance.TryGet(context.configID, out GameConfigs.ui_resRowData ui))
            {
                return;
            }
            
            // Bind Show
            var animType = GetAnimType(ui.AniShow);
            switch (animType)
            {
                case ANIM_TYPE.INTER:
                    context.doShowAnimation = UIAnimInter.Show;
                    break;
                
                case ANIM_TYPE.SCALE:
                    context.doShowAnimation = UIAnimScale.Show;
                    break;
            }
            
            // Bind Hide
            animType = GetAnimType(ui.AniHide);
            switch (animType)
            {
                case ANIM_TYPE.INTER:
                    context.doHideAnimation = UIAnimInter.Hide;
                    break;
                
                case ANIM_TYPE.SCALE:
                    context.doHideAnimation = UIAnimScale.Hide;
                    break;
            }
        }
    }
}