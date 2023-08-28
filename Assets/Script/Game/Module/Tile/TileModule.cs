using System.Collections.Generic;
using GameConfigs;
using log4net;
using Unity.Entities;
using UnityEngine;

namespace SGame
{
    public enum MapType : int
    {
        NORMAL = 0, // 默认的地块路径
        TRVAL  = 1, // 出行
    }
    
    public class TileModule : Singleton<TileModule>, IModule
    {
        private static ILog log = LogManager.GetLogger("xl.game");

        // 地块数据
        public class TileData
        {
            public int          tileId;             // 地块ID       
            public int          pos;                // 位置
            public int          buildingId;         // buildingID
            public Entity       buildingData;       // building 数据
            public GameObject   buildingRes;        // building 界面
        }

        // 位置信息
        private CheckPointData              m_checkPotinData1;
        
        /// <summary>
        /// 游戏世界
        /// </summary>
        private GameWorld                   m_gameWorld;

        /// <summary>
        /// 瓦片ID
        /// </summary>
        private List<TileData>                   m_tileDatas;

        /// <summary>
        /// 出行地块
        /// </summary>
        private List<TileData>                  m_trivalDatas;

        private MapType                         m_mapType       = MapType.NORMAL;

        public void Initalize(GameWorld gameWorld)
        {
            m_gameWorld      = gameWorld;
            m_tileDatas      = new List<TileData>();
        }

        /// <summary>
        /// 当前地图类型
        /// </summary>
        public MapType currentMap { get { return m_mapType; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapType"></param>
        /// <returns></returns>
        private List<TileData> GetMap(MapType mapType)
        {
            if (mapType == MapType.NORMAL)
                return m_tileDatas;

            return m_trivalDatas;
        }

        /// <summary>
        /// 关联建筑
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public bool ConnectBuilding(MapType map, int pos, GameObject gameObject)
        {
            // TRVAL
            TileData data = GetData(pos, map);
            if (data == null)
                return false;

            data.buildingRes = gameObject;
            return true;
        }

        /// <summary>
        /// 获取地块相关数据
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public TileData GetData(int pos, MapType mapType)
        {
            var mapData = GetMap(mapType);
            if (pos < 0 || pos > mapData.Count)
                return null;

            return mapData[pos];
        }

        /// <summary>
        /// 通过地图ID 加载地图
        /// </summary>
        /// <param name="id">地图路径ID</param>
        /// <param name="mapType">地图类型</param>
        /// <returns></returns>
        public bool LoadMap(int id, MapType mapType = MapType.NORMAL)
        {
            if (!ConfigSystem.Instance.TryGet(id, out Grid_PathRowData girdData))
            {
                log.Info("path not found = " + id.ToString());
                return false;
            }

            var mapData = GetMap(mapType);
            mapData.Clear();
            for (int i = 0; i < girdData.PositionLength; i++)
            {
                int tileId = girdData.Position(i);
                TileData data = CreateTileData(i, tileId);// new TileData();
                mapData.Add(data);
            }
            
            return true;
        }

        /// <summary>
        /// 创建tile数据
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="tileId"></param>
        /// <returns></returns>
        static TileData CreateTileData(int pos, int tileId)
        {
            TileData data = new TileData()
            {
                pos         = pos,
                tileId      = tileId
            };
            
            if (!ConfigSystem.Instance.TryGet(tileId, out GameConfigs.GridRowData girdData))
            {
                log.Error("tile Id not found=" + tileId);
                return data;
            }

            data.buildingId = girdData.EventBuildId;
            data.buildingData = BuildingModule.Instance.GetBuilding(data.buildingId);
            return data;
        }

        /// <summary>
        /// 通过格子ID获得TILE ID
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public int GetTileIdByPos(int pos, MapType mapType)
        {
            return GetData(pos, mapType).tileId;
        }

        /// <summary>
        /// 通过位置获得建筑ID
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public int GetBuildingIDByPos(int pos, MapType mapType)
        {
            return GetData(pos, mapType).buildingId;
        }

        public void Update()
        {

        }

        public void Shutdown()
        {
            m_tileDatas.Clear();
            m_trivalDatas.Clear();
        }
    }
}
