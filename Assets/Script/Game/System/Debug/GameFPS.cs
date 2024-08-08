using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SGame;

public class GameFPS : MonoBehaviour
{
    //#ifdef GAME_FPS
    // Start is called before the first frame update
    public float        fps            ;
    public float        sampleTime  = 1f; // 采样时间
    public int          counter     = 0;     // update 计数
    public Color        textColor   = Color.red;
    public Color        backgroundColor = Color.black;
    public int          font_size = 30;
    private float       lastTime    = 0;
    private GUIStyle    guiStyle       ;
    
    public Vector2      drawPos = Vector2.zero;
    
    void Start()
    {
        int screenWidth = Screen.width;
        font_size = (int)(font_size * screenWidth / 750f);
        guiStyle = new GUIStyle();
        guiStyle.normal.textColor = textColor;
        guiStyle.fontSize = font_size;
        #if DISABLE_FPS
            Destroy(this);
        #else
            Game.console.AddCommand("fps", Cmd, "Show FPS Command");
        #endif
    }
    
    void Cmd(string[] args)
    {
        this.enabled = !this.enabled;
    }
    
    // Update is called once per frame
    void Update()
    {
        counter++;
        float t = Time.realtimeSinceStartup;
        if (t - lastTime >= sampleTime)
        {
            float duration = t - lastTime;
            fps = counter / duration;
            counter = 0;
            lastTime = t;
        }
        
        // 显示FPS
        DebugOverlay.SetColor(textColor);
        var oldPos = DebugOverlay.GetOrigin();
        DebugOverlay.SetOrigin(drawPos.x, drawPos.y);
        DebugOverlay.DrawRect(-1, 0, 12, 2, backgroundColor);
        DebugOverlay.Write(0, 0, "FPS:{0,6:###.##}", fps);
        
        // 显示订单数量
        DebugOverlay.Write(0, 1, "TASK:{0,-4}", OrderTaskManager.Instance.TaskCount);
        DebugOverlay.SetOrigin(oldPos.x, oldPos.y);
    }
}
