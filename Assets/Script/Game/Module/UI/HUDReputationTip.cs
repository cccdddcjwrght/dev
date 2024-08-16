using log4net;
using SGame.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDReputationTip : IUIScript
{
    public static IUIScript Create() { return new HUDReputationTip(); }
    private static ILog log = LogManager.GetLogger("game.hud");

    public void OnInit(UIContext context)
    {
        context.onUninit += OnClose;
        context.window.contentPane.touchable = false;
    }

    private void OnClose(UIContext obj)
    {
        
    }
}
