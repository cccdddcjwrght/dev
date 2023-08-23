using System.Collections.Generic;
using log4net;
using Unity.Entities;

namespace SGame
{
    /// <summary>
    /// 建筑系统
    /// </summary>
    public class BuildingModule : Singleton<BuildingModule>, IModule
    {
        private ILog log = LogManager.GetLogger("xl.game.building");
        
        public void Initalize(GameWorld gameWorld)
        {
            m_gameWorld = gameWorld;
            m_datas     = new Dictionary<int, Entity>(32);
            
            CreateBuilds();
        }

        /// <summary>
        /// 设置
        /// </summary>
        void CreateBuilds()
        {
            var archetypeBase = EntityManager.CreateArchetype( typeof(BuildingData));
            GameConfigs.Event_Build values = ConfigSystem.Instance.LoadConfig<GameConfigs.Event_Build>();
            for (int i = 0; i < values.DatalistLength; i++)
            {
                var data = values.Datalist(i);
                if (m_datas.ContainsKey(data.Value.Id))
                {
                    log.Error("build id repeate = " + data.Value.Id);
                    continue;
                };
                
                // 生成默认银行
                var e      = EntityManager.CreateEntity(archetypeBase);
                var buildData   = new BuildingData()                            { id = data.Value.Id, level = 1 , buildType = data.Value.EventType};

                int buildingEventId = data.Value.BuildId(buildData.level - 1);

                switch (data.Value.EventType)
                {
                    case (int)Cs.EventType.Gold:
                        // 服务器已处理
                        break;
                    case (int)Cs.EventType.Bank:
                        AddBankData(e, buildingEventId);
                        break;
                }
                
                EntityManager.SetComponentData(e, buildData);
                m_datas.Add(buildData.id, e);
            }
        }
        
        /// <summary>
        /// 添加银行数据
        /// </summary>
        /// <param name="e"></param>
        /// <param name="buildEventId"></param>
        void AddBankData(Entity e, int buildEventId)
        {
            if (!ConfigSystem.Instance.TryGet(buildEventId, out GameConfigs.Build_BankRowData data))
            {
                log.Error("bank build event id not found=" + buildEventId);
                return;
            }
            
            // 设置银行初始值
            var bankData    = new BuildingBankData()                        { Value = data.BasicRewardsCoin };
            EntityManager.AddComponent<BuildingBankData>(e);
            EntityManager.SetComponentData(e, bankData);
        }
        

        public int GetBuildEventId(int buildId, int level)
        {
            return Utils.GetBuildingEventId(buildId, level);
        }

        public void Shutdown()
        {
            
        }
        
        EntityManager EntityManager { get { return m_gameWorld.GetEntityManager(); } }

        /// <summary>
        /// 获得建筑数据
        /// </summary>
        /// <param name="buildId"></param>
        /// <returns></returns>
        public Entity GetBuilding(int buildId, int playerId = 0)
        {
            if (m_datas.TryGetValue(buildId, out Entity e) == false)
                return Entity.Null;

            return e;
        }

        /// <summary>
        /// 判断是有建筑数据
        /// </summary>
        /// <param name="buildId"></param>
        /// <param name="playerId"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool HasBuidlingData<T>(int buildId, int playerId = 0) where T : struct, IComponentData
        {
            Entity e = GetBuilding(buildId);
            if (e == Entity.Null)
                return false;

            return EntityManager.HasComponent<T>(e);
        }

        /// <summary>
        /// 获取建筑数据
        /// </summary>
        /// <param name="buildId"></param>
        /// <param name="playerId"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetBuildingData<T>(int buildId, int playerId = 0) where T : struct, IComponentData
        {
            Entity e = GetBuilding(buildId);
            if (e == Entity.Null)
            {
                log.Error("Data Not Found=" + buildId.ToString());
                return default;
            }
                
            return EntityManager.GetComponentData<T>(e);
        }
        
        /// <summary>
        /// 设置建筑数据
        /// </summary>
        /// <param name="buildId"></param>
        /// <param name="value"></param>
        /// <param name="playerId"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool SetBuildingData<T>(int buildId, T value, int playerId = 0) where T : struct, IComponentData
        {
            Entity e = GetBuilding(buildId);
            if (e == Entity.Null)
            {
                log.Error("Data Not Found=" + buildId.ToString());
                return false;
            }
            
            EntityManager.SetComponentData(e, value);
            return true;
        }

        /// <summary>
        /// 建筑数据
        /// </summary>
        public Dictionary<int, Entity>  m_datas;

        private GameWorld               m_gameWorld;
    }
}
