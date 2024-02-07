using System.Collections;
using System.Collections.Generic;
using SGame.UI;
using UnityEngine;
using log4net;
using SGame;
using SGame.UI.Hud;
using UIUtils = SGame.UIUtils;

public class HUDUpdate : IUIScript
{
    public static IUIScript Create() { return new HUDUpdate(); }
    private static ILog log = LogManager.GetLogger("game.hud");
    private UI_Update updateUI;

    public void OnInit(UIContext context)
    {
        context.window.contentPane.touchable = false;
        updateUI = context.content as UI_Update;
    }
    
}
