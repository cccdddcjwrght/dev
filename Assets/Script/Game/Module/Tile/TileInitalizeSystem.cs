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
            Entities.WithNone<InitalizeTag>().ForEach((Entity e, TileData tileData, in Translation translation) =>
            {
                TileMap m = m_tileModule.GetMap(tileData.mapType);
                tileData.Position3d = translation.Value;
                m.Register(tileData);
                commandBuffer.AddComponent<InitalizeTag>(e);
            }).WithoutBurst().Run();
        }
    }
}
