using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

namespace SGame.UI
{
    /// <summary>
    /// 实现UI缩放动画效果
    /// </summary>
    public class UIAnimScale
    {
        private const float TWEEN_TIME = 0.3f;
        
        /// <summary>
        /// 缩放
        /// </summary>
        /// <param name="context"></param>
        public static void Show(UIContext context)
        {
            var ui = context.window.contentPane;
			var content =  ui.GetChild("content")?.asCom ;
            if (content != null)
                ui = content;
            
            var tween = GTween.To(new Vector2(0, 0), new Vector2(1, 1), TWEEN_TIME);
            tween.SetTarget(ui, TweenPropType.Scale);
            tween.SetEase(EaseType.BackOut);
            tween.OnComplete(() => context.window.FinishAnimation(true));

            var tween2 = GTween.To(0, 1, TWEEN_TIME);
            ui.alpha = 0;
            tween2.SetTarget(ui, TweenPropType.Alpha);
            tween2.SetEase(EaseType.QuadOut);
        }
        
        public static void Hide(UIContext context)
        {
            var ui = context.window.contentPane;
            var content = ui.GetChild("content")?.asCom ;
            if (content != null)
                ui = content;

            var tween = GTween.To(new Vector2(1, 1), new Vector2(0, 0), TWEEN_TIME);
            tween.SetTarget(ui, TweenPropType.Scale);

            tween.SetEase(EaseType.BackIn);
            tween.OnComplete(() => context.window.FinishAnimation(false));

            var tween2 = GTween.To(1, 0, TWEEN_TIME);
            tween2.SetTarget(ui, TweenPropType.Alpha);
            tween2.SetEase(EaseType.QuadOut);
            tween2.SetDelay(0.1f);
        }
    }
}
