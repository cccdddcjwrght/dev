using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SGame;

public class GameQuatilySetting : MonoSingleton<GameQuatilySetting>
{
    public int quatily = 0;
    public int LOD = 0;
    private int m_oldValue = 0;
    private int m_oldLOD;

    /// <summary>
    /// 外部设置
    /// </summary>
    /// <param name="level"></param>
    public void SetQualityLevel(int level)
    {
        quatily = level;
        if (quatily >= 3)
        {
            LOD = 300;
        }
        else
        {
            LOD = 100;
        } 
        
        QualitySettings.SetQualityLevel(level);
        Shader.globalMaximumLOD = LOD;

        m_oldValue  = level;
        m_oldLOD    = LOD;
        
        EventManager.Instance.Trigger((int) GameEvent.QUATILY_CHANGE, quatily);
    }


    // Start is called before the first frame update
    void Start()
    {
        int cpuSpeed = SystemInfo.processorFrequency;
        Debug.Log("CPU SPEED=" + cpuSpeed);

        quatily         = QualitySettings.GetQualityLevel();
        m_oldValue      = quatily;
        LOD             = Shader.globalMaximumLOD;
        m_oldLOD        = LOD;
    }

    // Update is called once per frame
    void Update()
    {
        if (quatily != m_oldValue)
        {
            m_oldValue = quatily;
            QualitySettings.SetQualityLevel(quatily);
            EventManager.Instance.Trigger((int) GameEvent.QUATILY_CHANGE, quatily);
        }

        if (LOD != m_oldLOD)
        {
            m_oldValue = LOD;
            Shader.globalMaximumLOD = LOD;
            EventManager.Instance.Trigger((int) GameEvent.QUATILY_CHANGE, quatily);
        }
    }
}
