using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame.UI
{
    /// <summary>
    /// 实现UI内部动画效果
    /// </summary>
    public class UIAnimInter
    {
        public static void Show(UIContext context)
        {
            var ui = context.window.contentPane;
            var trans = ui.GetTransition("doshow");
            if (trans == null)
            {
                context.window.FinishAnimation(true);
                return;
            }
            
            trans.Play(1, 0, ()=> context.window.FinishAnimation(true)) ;
        }
        
        public static void Hide(UIContext context)
        {
            var ui = context.window.contentPane;
            var trans = ui.GetTransition("dohide");
            if (trans == null)
            {
                context.window.FinishAnimation(false);
                return;
            }
            
            trans.Play(1, 0, ()=> context.window.FinishAnimation(false)) ;
        }
    }
}
