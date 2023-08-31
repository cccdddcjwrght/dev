using System.Collections.Generic;
using GameConfigs;
using log4net;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace SGame
{

    
    public class TileModule : Singleton<TileModule>, IModule
    {
        private static ILog log = LogManager.GetLogger("xl.game");

        /// <summary>
        /// 游戏世界
        /// </summary>
        private GameWorld                   m_gameWorld;

        /// <summary>
        /// 瓦片ID
        /// </summary>
        private TileMap                   m_mapNormal;

        /// <summary>
        /// 出行地块
        /// </summary>
        private TileMap                  m_mapTrival;

        private MapType                  m_mapType       = MapType.NORMAL;

        private TileInitalizeSystem     m_initalizeSystem;

        public void Initalize(GameWorld gameWorld)
        {
            m_gameWorld      = gameWorld;
            m_mapNormal      = new TileMap(gameWorld);
            m_mapTrival      = new TileMap(gameWorld);
            m_initalizeSystem = gameWorld.GetECSWorld().CreateSystem<TileInitalizeSystem>();
            m_initalizeSystem.Initalize(this);
        }

        /// <summary>
        /// 当前地图类型
        /// </summary>
        public MapType currentMap { get { return m_mapType; } }

        /// <summary>
        /// 地块数量
        /// </summary>
        public int tileCount
        {
            get
            {
                return GetMap(currentMap).count;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapType"></param>
        /// <returns></returns>
        public TileMap GetMap(MapType mapType)
        {
            if (mapType == MapType.NORMAL)
                return m_mapNormal;

            return m_mapTrival;
        }

        /// <summary>
        /// 当前地图
        /// </summary>
        public TileMap current
        {
            get
            {
                return GetMap(m_mapType);
            }
        }

        /// <summary>
        /// 获取地块相关数据
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public TileData GetData(int pos)
        {
            var mapType = currentMap;
            var mapData = GetMap(mapType);
            if (pos < 0 || pos >= mapData.count)
            {
                log.Error("Pos Not Found=" + pos + " mapType=" + mapType);
                return null;
            }

            return mapData.GetTileDataFromPos(pos);// [pos];
        }

        /// <summary>
        /// 通过地图ID 加载地图
        /// </summary>
        /// <param name="id">地图路径ID</param>
        /// <param name="mapType">地图类型</param>
        /// <returns></returns>
        public bool LoadMap(int id, MapType mapType = MapType.NORMAL)
        {
            var m = GetMap(mapType);
            m_mapType = mapType;
            return m.LoadMap(id);
        }

        /// <summary>
        /// 用户恢复地图
        /// </summary>
        /// <param name="mapType"></param>
        public void SetMapType(MapType mapType)
        {
            m_mapType = mapType;
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
            //data.buildingData = BuildingModule.Instance.GetBuilding(data.buildingId);
            return data;
        }

        /// <summary>
        /// 通过格子ID获得TILE ID
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public int GetTileIdByPos(int pos)
        {
            return GetData(pos).tileId;
        }

        /// <summary>
        /// 通过位置获得建筑ID
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public int GetBuildingIDByPos(int pos, MapType mapType)
        {
            return GetData(pos).buildingId;
        }

        public void Update()
        {
            m_initalizeSystem.Update();
        }

        public void Shutdown()
        {
            m_mapNormal.Shutdown();
            m_mapTrival.Shutdown();
            m_gameWorld.GetECSWorld().DestroySystem(m_initalizeSystem);
        }
    }
}
