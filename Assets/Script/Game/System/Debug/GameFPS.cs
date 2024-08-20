using System;
using System.Collections;
using System.Collections.Generic;
using log4net;
using UnityEngine;
using SGame;
using Unity.Entities;

public class GameFPS : MonoBehaviour
{
    private static ILog log = LogManager.GetLogger("debug");
    
    public Color        textColor   = Color.red;
    public Color        backgroundColor = Color.black;
    
    public Vector2      drawPos = Vector2.zero;


    private bool m_enableTask = false;
    private FPSData m_fpsData = new FPSData();
    
    class FPSData
    {
        public const float TICK_TIME = 1.0f;
        public float    fpsTick = 0f;
        public float[]  timerRecord = new float[128];
        public bool     enable = false;
        public int      index;
        public int      callCount;

        public float    fps;
        public float    minFps;
        public float    maxFps;
        public float    avgFps;
        public float    lowerFps; // 低帧率百分比
        
        /// <summary>
        /// 显示帧率
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        static float GetFps(float t)
        {
            if (t <= 0.0001f)
                return 9999;

            return 1.0f / t;
        }

        public void Update(float deltaTime)
        {
            callCount++;
            fpsTick += deltaTime;
            index = (index + 1) % timerRecord.Length;
            timerRecord[index] = deltaTime;

            if (fpsTick >= TICK_TIME)
            {
                fps = (float)callCount / fpsTick;
                callCount = 0;
                fpsTick = 0;
                UpdateFpsDetail();
            }
        }

        void UpdateFpsDetail()
        {
            float minTime = float.MaxValue;
            float maxTime = float.MinValue;
            float avg = 0;
            int lowerCount = 0;
            foreach (var t in timerRecord)
            {
                avg += t;

                if (t < minTime)
                    minTime = t;

                if (t > maxTime)
                    maxTime = t;

                if (t > 0.05f)
                    lowerCount++;
            }

            minFps = GetFps(maxTime);
            avgFps = GetFps(avg / timerRecord.Length);
            maxFps = GetFps(minTime);
            lowerFps = lowerCount * 100.0f / timerRecord.Length;
        }
    }

    private const string SETTING_FPS_NAME = "setting.fps";
    
    void Start()
    {
        Game.console.AddCommand("show", Cmd, "show [fps|task] Command");
        m_fpsData.enable = PlayerPrefs.GetInt(SETTING_FPS_NAME, 0) != 0;
    }
    
    void Cmd(string[] args)
    {
        if (args == null || args.Length == 0)
        {
            //log.Error("args invalid");
            this.enabled = false;
            m_fpsData.enable = false;
            m_enableTask = false;
            return;
        }

        switch (args[0])
        {
            case "fps":
                m_fpsData.enable = !m_fpsData.enable;
                if (m_fpsData.enable) 
                    this.enabled = true;

                PlayerPrefs.SetInt(SETTING_FPS_NAME, m_fpsData.enable ? 1 : 0);
                break;
            
            case "task":
                m_enableTask = !m_enableTask;
                if (m_enableTask)
                    this.enabled = true;
                break;
            
            default:
                Game.console.Write("not support command");
                log.Error("not support command");
                break;
        }
    }
    
    float ShowFps(float y)
    {
        m_fpsData.Update(Time.deltaTime);
        
        DebugOverlay.DrawRect(-1, y, 36, 2, backgroundColor);
        DebugOverlay.Write(0, y, "fps{0,6:###.##}, lower={1, 6:###.##}%", 
            m_fpsData.fps,
            m_fpsData.lowerFps);
        
        DebugOverlay.Write(0, y + 1, "min{0,6:###.##} avg{1, 6:###.##} max{2,6:###.##}", 
            m_fpsData.minFps,
            m_fpsData.avgFps,
            m_fpsData.maxFps);

        return y + 2;
    }

    void ShowTask(float y)
    {
        // 显示订单数量
        DebugOverlay.DrawRect(-1, y, 12, 1, backgroundColor);
        DebugOverlay.Write(0, y, "TASK:{0,-4}", OrderTaskManager.Instance.TaskCount);
    }
    
    // Update is called once per frame
    void Update()
    {
        // 显示FPS
        DebugOverlay.SetColor(textColor);
        var oldPos = DebugOverlay.GetOrigin();
        DebugOverlay.SetOrigin(drawPos.x, drawPos.y);

        float ypos = 0;
        if (m_fpsData.enable)
            ypos = ShowFps(ypos);

        if (m_enableTask)
            ShowTask(ypos++);

        DebugOverlay.SetOrigin(oldPos.x, oldPos.y);
    }
}
