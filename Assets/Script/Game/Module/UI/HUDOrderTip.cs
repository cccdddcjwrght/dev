using System.Collections;
using System.Collections.Generic;
using GameConfigs;
using SGame.UI;
using UnityEngine;
using log4net;
using SGame;
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
        FoodType type = context.gameWorld.GetEntityManager().GetComponentData<FoodType>(context.entity);
        if (ConfigSystem.Instance.TryGet<ItemRowData>((c) => (
                    (ItemRowData)c).ItemId == type.Value,
                out var foodcfg))
        {
            _uiOrderTipUI.m_icon.url=string.Format("ui://Common/{0}",foodcfg.Icon);
        }

        _uiOrderTipUI.m_num.text = type.num.ToString();
    }
    
}
