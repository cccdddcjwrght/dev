using System.Collections;
using System.Collections.Generic;
using SGame.UI;
using UnityEngine;
using log4net;
using SGame;
using SGame.UI.Hud;
using Unity.Transforms;

public class HUDFloatText : IUIScript
{
    public static IUIScript Create() { return new HUDFloatText(); }
    private static ILog log = LogManager.GetLogger("game.hud");
    private UI_FloatText _uiFloatText;

    public void OnInit(UIContext context)
    {
        context.window.contentPane.touchable = false;
        _uiFloatText = context.content as UI_FloatText;
        HUDTips tip = context.gameWorld.GetEntityManager().GetComponentObject<HUDTips>(context.entity);
        _uiFloatText.m_title.text = "+"+tip.title;
        context.onUpdate += Update;
    }

    void Update(UIContext context)
    {
    }
    
    
    
}
