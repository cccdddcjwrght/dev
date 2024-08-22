using SGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ս������ʱ��
/// (��������ս�����ٶ�)
/// </summary>
public class BattleTimer : Singleton<BattleTimer>
{
    private float startTime;
    private float timeScale;
    private float passTime;

    public void Start() 
    {
        startTime = Time.realtimeSinceStartup;
        timeScale = 1;
        passTime = 0;
    }

    public void SetScale(float scale) 
    {
        if (scale < 0) return;

        if (timeScale == scale) return;
        passTime = passTime + (Time.realtimeSinceStartup - startTime) * timeScale;
        startTime = Time.realtimeSinceStartup;
        timeScale = scale;
    }

    public float CurTime() 
    {
        return passTime + (Time.realtimeSinceStartup - startTime) * timeScale;
    }
}
