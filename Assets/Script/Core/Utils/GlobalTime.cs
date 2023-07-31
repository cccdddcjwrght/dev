using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

// 用于获得高精度的计时器
public class GlobalTime
{
    /// <summary>
    /// 时间计数基础模块
    /// </summary>
    static Stopwatch s_Clock;

    /// <summary>
    /// 高进度计时器的频率是多少 (1秒几次计数)
    /// </summary>
    static long      s_Frequency = 1;

    /// <summary>
    /// 统计上一帧的计数器
    /// </summary>
    static long      s_lastTick  = 0;

    /// <summary>
    /// 获得帧间隔时间
    /// </summary>
    static double    s_deltaTime = 0;  

    /// <summary>
    /// 返回从统计开始到现在的 tick 数
    /// </summary>
    public static long ElapsedTicks { get { return s_Clock.ElapsedTicks; } }

    /// <summary>
    /// 当前帧时间
    /// </summary>
    public static double deltaTime { get { return s_deltaTime; } }

    /// <summary>
    /// 获得从统计开始到现在的时间 （秒)
    /// </summary>
    public static double passTime { get { return (double)s_Clock.ElapsedTicks / s_Frequency; } }

    public static bool isInitalize
    {
        get
        {
            return s_Clock != null;
        }
    }

    /// <summary>
    /// 开始计时
    /// </summary>
    public static void Start()
    {
        if (s_Clock == null)
            s_Clock = new Stopwatch();

        s_Clock.Start();
        s_Frequency = Stopwatch.Frequency;
        s_deltaTime = 0;
        s_lastTick = 0;
    }

    /// <summary>
    /// 刷新当前帧时间
    /// </summary>
    public static void UpdateFrameTime()
    {
        if (s_Clock == null)
            return;

        long lElapsedTicks = s_Clock.ElapsedTicks;
        s_deltaTime = (double)(lElapsedTicks - s_lastTick) / s_Frequency;

        s_lastTick = lElapsedTicks;
    }

    /// <summary>
    /// 停止
    /// </summary>
    public static void Stop()
    {
        s_Clock.Stop();
    }
}
