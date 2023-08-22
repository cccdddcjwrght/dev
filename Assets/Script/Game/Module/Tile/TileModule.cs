using System.Collections;
using System.Collections.Generic;
using SGame;
using UnityEngine;

public class TileModule : Singleton<TileModule>, IModule
{
    // 位置到地块ID
    public Dictionary<int, int> m_posToTileID;

    private CheckPointData      m_checkPotinData1;

    public void Initalize(GameWorld gameWorld)
    {
        
    }

    // 加载地图ID
    public void LoadMap(int id)
    {
        
    }

    // 通过格子ID获得TILE ID
    public int GetTileIdByPos(int pos)
    {
        return pos + 1;
    }
    
    
    public void Update()
    {
        
    }
    
    public void Shutdown()
    {
        
    }
    
}
