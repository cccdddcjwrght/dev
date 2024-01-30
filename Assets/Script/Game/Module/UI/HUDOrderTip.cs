using System.Collections;
using System.Collections.Generic;
using SGame.UI;
using UnityEngine;
using log4net;
using SGame.UI.Hud;

public class HUDOrderTip : IUIScript
{
    public static IUIScript Create() { return new HUDOrderTip(); }
    private static ILog log = LogManager.GetLogger("game.hud");
    private UI_OrderTip _uiOrderTipUI;

    public void OnInit(UIContext context)
    {
        context.window.contentPane.touchable = false;
        _uiOrderTipUI = context.content as UI_OrderTip;
    }
    
}
