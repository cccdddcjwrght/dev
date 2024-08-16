using System.Collections;
using System.Collections.Generic;
using GameConfigs;
using SGame.UI;
using UnityEngine;
using log4net;
using SGame;
using SGame.UI.Hud;
using Unity.VisualScripting;

public class HUDOrderTip : IUIScript
{
    public static IUIScript Create() { return new HUDOrderTip(); }
    private static ILog log = LogManager.GetLogger("game.hud");
    private UI_OrderTip _uiOrderTipUI;

    public void OnInit(UIContext context)
    {
        context.window.contentPane.touchable = false;
        _uiOrderTipUI = context.content as UI_OrderTip;
        context.window.AddEventListener("OrderNumUpdate", OnFoodNumUpdate);
        context.onOpen += OnOpen;
        OnOpen(context);
    }

    void OnOpen(UIContext context)
    {
        FoodItem item = context.gameWorld.GetEntityManager().GetComponentData<FoodItem>(context.entity);
        if (ConfigSystem.Instance.TryGet<ItemRowData>((c) => (
                (ItemRowData)c).ItemId == item.itemID,
            out var foodcfg))
        {
            _uiOrderTipUI.m_icon.url=string.Format("ui://Common/{0}",foodcfg.Icon);
        }
        _uiOrderTipUI.m_num.text = item.num.ToString();
        _uiOrderTipUI.m_friend.selectedIndex = item.isFriend ? 1 : 0;
    }
    
    void OnFoodNumUpdate(FairyGUI.EventContext uiContext)
    {
        SetNumText(uiContext.data.ToString());
    }
    
    /// <summary>
    /// 刷新数字
    /// </summary>
    /// <param name="data"></param>
    void SetNumText(string data)=>UIListener.SetText(_uiOrderTipUI.m_num,data);
}
