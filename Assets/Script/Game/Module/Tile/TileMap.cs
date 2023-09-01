using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using GameConfigs;
using log4net;

namespace SGame
{
    public enum MapType : int
    {
        NORMAL = 0, // 默认的地块路径
        TRVAL  = 1, // 出行
    }

    /// <summary>
    /// 地块地图信息 
    /// </summary>
    public class TileMap
    {
        private static ILog log = LogManager.GetLogger("game.tile");
        private GameWorld m_gameWorld;
        private int m_mapId;

        public TileMap(GameWorld gameWorld)
        {
            m_gameWorld = gameWorld;
            m_paths = new List<TileData>();
            m_datas = new Dictionary<int, TileData>();
            m_mapId = 0;
        }

        public EntityManager entityManager
        {
            get
            {
                return m_gameWorld.GetEntityManager();
            }
        }

        public int mapId
        {
            get { return m_mapId; }
        }
        
        /// <summary>
        /// 注册Tile
        /// </summary>
        /// <param name="tileId"></param>
        /// <param name="tileEntity"></param>
        /// <returns></returns>
        public bool Register(TileData tileData)
        {
            if (m_datas.ContainsKey(tileData.tileId))
            {
                log.Error("tile id repeate=" + tileData.tileId.ToString());
                return false;
            }

            m_datas.Add(tileData.tileId, tileData);
            return true;
        }

        /// <summary>
        /// 加载地图
        /// </summary>
        /// <param name="mapId"></param>
        public bool LoadMap(int mapId)
        {
            if (!ConfigSystem.Instance.TryGet(mapId, out Grid_PathRowData girdData))
            {
                log.Info("path not found = " + mapId.ToString());
                return false;
            }

            var mapData = m_paths;
            mapData.Clear();
            for (int i = 0; i < girdData.PositionLength; i++)
            {
                int tileId = girdData.Position(i);
                TileData data = GetTileDataFromId(tileId);
                data.pos = i;
                mapData.Add(data);
            }

            m_mapId = mapId;
            return true;
        }

        /// <summary>
        /// 通过位置获得瓦片数据
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public TileData GetTileDataFromId(int id)
        {
            if (m_datas.TryGetValue(id, out TileData data))
                return data;

            return null;
        }
        
        /// <summary>
        /// 通过位置获得瓦片数据
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public TileData GetTileDataFromPos(int pos)
        {
            return m_paths[pos];
        }

        /// <summary>
        /// 地形数量
        /// </summary>
        public int count { get { return m_paths.Count; } }

        private List<TileData>            m_paths;
        private Dictionary<int, TileData> m_datas;



        public void Shutdown()
        {
            
        }
    }
}