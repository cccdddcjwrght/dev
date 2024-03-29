using System.Collections;
using System.Collections.Generic;
using SGame.UI;
using UnityEngine;
using log4net;
using SGame;
using SGame.UI.Hud;
using Unity.Entities;
using UIUtils = SGame.UIUtils;

public class HUDEffect : IUIScript
{
    public static IUIScript Create() { return new HUDEffect(); }
    private static ILog log = LogManager.GetLogger("game.hud");
   // private UI_Effect effectUI;
    private Entity effect;

    public void OnInit(UIContext context)
    {
        context.onClose += OnClose;
        context.window.contentPane.touchable = false;
        //effectUI = context.content as UI_Effect;
        //effect = EffectSystem.Instance.AddEffect(8, effectUI);
    }

    private void OnClose(UIContext obj)
    {
        if (effect != Entity.Null)
        {
            EffectSystem.Instance.CloseEffect(effect);
        }
    }
}
