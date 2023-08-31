using System.Collections.Generic;
using GameConfigs;
using log4net;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace SGame
{
    /// <summary>
    /// 对tile 进行初始化
    /// </summary>
    [DisableAutoCreation]
    public partial class TileInitalizeSystem : SystemBase
    {
        private TileModule m_tileModule;
        private EndSimulationEntityCommandBufferSystem m_commandBuffer;
        private static ILog log = LogManager.GetLogger("xl.game.tile");
        
        /// <summary>
        /// 初始化标记
        /// </summary>
        public struct InitalizeTag : IComponentData
        {
        }


        protected override void OnCreate()
        {
            m_commandBuffer = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }

        public void Initalize(TileModule tileModule)
        {
            m_tileModule = tileModule;
        }
        
        protected override void OnUpdate()
        {
            var commandBuffer = m_commandBuffer.CreateCommandBuffer();
            Entities.WithNone<InitalizeTag>().ForEach((Entity e, TileData tileData) =>
            {
                TileMap m = m_tileModule.GetMap(tileData.mapType);
                //tileData.Position3d = translation.Value;
                if (!ConfigSystem.Instance.TryGet(tileData.tileId, out GameConfigs.GridRowData girdData))
                {
                    log.Error("tile Id not found=" + tileData.tileId);
                    return;
                }

                tileData.buildingId = girdData.EventBuildId;
                m.Register(tileData);
                commandBuffer.AddComponent<InitalizeTag>(e);
            }).WithoutBurst().Run();
        }
    }
}
