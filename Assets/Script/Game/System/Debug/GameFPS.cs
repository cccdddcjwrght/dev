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
        
        DebugOverlay.SetColor(textColor);
        DebugOverlay.SetOrigin(0, 0);
        DebugOverlay.DrawRect(drawPos.x - 1, 0, 12, 1, backgroundColor);
        DebugOverlay.Write(drawPos.x, drawPos.y, "FPS:{0,6:###.##}", fps);
    }

    /*
    private void OnGUI()
    {
        GUI.Label(new Rect(Screen.width / 2, Mathf.Min(Screen.height / 100, 30) , 100, 100),"FPS=" + fps , guiStyle);
    }
    */
    //#endif
}
