using System.Collections;
using System.Collections.Generic;
using SGame.UI;
using UnityEngine;
using log4net;
using SGame.UI.Hud;

public class HUDProgress : IUIScript
{
    public static IUIScript Create() { return new HUDProgress(); }
    private static ILog log = LogManager.GetLogger("game.hud");
    private UI_Progress progressUI;

    public void OnInit(UIContext context)
    {
        context.window.contentPane.touchable = false;
        progressUI = context.content as SGame.UI.Hud.UI_Progress;
        context.onUpdate += Update;
        //log.Info(string.Format("HUDProgress Init XY Value = {0}", ui.m_n0.xy));
    }

    void Update(UIContext context)
    {
        progressUI.m_n1.fillAmount = progressUI.m_n1.fillAmount + Time.deltaTime;
        if (progressUI.m_n1.fillAmount > 1)
            progressUI.m_n1.fillAmount = 0;
    }
}
