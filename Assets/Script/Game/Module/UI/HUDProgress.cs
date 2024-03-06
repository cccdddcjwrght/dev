using System.Collections;
using System.Collections.Generic;
using SGame.UI;
using UnityEngine;
using log4net;
using SGame;
using SGame.UI.Hud;
using UIUtils = SGame.UIUtils;

public class HUDProgress : IUIScript
{
    public static IUIScript Create() { return new HUDProgress(); }
    private static ILog log = LogManager.GetLogger("game.hud");
    private UI_Progress progressUI;
    private float time;
    private float updateTime;

    public void OnInit(UIContext context)
    {
        context.window.contentPane.touchable = false;
        progressUI = context.content as UI_Progress;
        time = context.gameWorld.GetEntityManager().GetComponentData<TweenTime>(context.entity).Value;
        updateTime = 0;
        context.onUpdate += Update;
    }

    void Update(UIContext context)
    {
        if (updateTime <= time)
        {
            updateTime += Time.deltaTime;
            float fillPercentage = CalculateFillPercentage(updateTime, time);
            progressUI.m_progress.fillAmount = fillPercentage;
        }
        else
        {
            UIUtils.CloseUI(context.entity);
        }
    }
    
    /// <summary>
    /// 线性计算填充比例
    /// </summary>
    /// <param name="updateTime"></param>
    /// <param name="totalTime"></param>
    /// <returns></returns>
    float CalculateFillPercentage(float updateTime, float totalTime) {
        float fillPercentage = updateTime / totalTime; 
        fillPercentage = Mathf.Clamp01(fillPercentage); 
        return fillPercentage;
    }
}
