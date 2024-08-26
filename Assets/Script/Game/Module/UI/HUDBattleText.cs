using System.Collections;
using System.Collections.Generic;
using SGame.UI;
using UnityEngine;
using log4net;
using SGame;
using SGame.UI.Hud;
using Unity.Transforms;

public class HUDBattleText : IUIScript
{
    public static IUIScript Create() { return new HUDBattleText(); }
    private static ILog log = LogManager.GetLogger("game.hud");
    private UI_BattleText _uiBattleText;
    private HUDTips _hudTips;

    public void OnInit(UIContext context)
    {
        context.content.touchable = false;
        _uiBattleText = context.content as UI_BattleText;

        _hudTips = context.gameWorld.GetEntityManager().GetComponentObject<HUDTips>(context.entity);
        _uiBattleText.m_title.color = _hudTips.color;

        context.onOpen += OnOpen;
        context.rootUI.SetXY(_hudTips.offsetX, _hudTips.offsetY);
        context.rootUI.z = -150;
    }

    void OnOpen(UIContext context)
    {
        _uiBattleText.m_title.text = _hudTips.title;
    }
}
