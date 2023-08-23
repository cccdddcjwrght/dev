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

    // 通过地图ID 加载地图
    public void LoadMap(int id)
    {
        
    }

    // 通过格子ID获得TILE ID
    public int GetTileIdByPos(int pos)
    {
        return pos + 1;
    }

    /// <summary>
    /// 通过位置获得场景
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public int GetBuildingIDByPos(int pos)
    {
        int tileId = GetTileIdByPos(pos);
        if (tileId <= 0)
            return -1;

        if (!ConfigSystem.Instance.TryGet(tileId, out GameConfigs.GridRowData girdData))
        {
            return -1;
        }

        return girdData.EventBuildId;
    }
    
    public void Update()
    {
        
    }
    
    public void Shutdown()
    {
        
    }
    
}
