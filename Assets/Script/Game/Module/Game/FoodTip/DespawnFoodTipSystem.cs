using Unity.Entities;

namespace SGame
{
    /// <summary>
    /// 食物小费管理
    /// </summary>
    [UpdateInGroup(typeof(GameLogicAfterGroup))]
    public partial class DespawnFoodTipSystem : SystemBase
    {
        private EndSimulationEntityCommandBufferSystem m_commandBuffer;

        protected override void OnCreate()
        {
            m_commandBuffer = World.GetOrCreateSystemManaged<EndSimulationEntityCommandBufferSystem>();
        }

        protected override void OnUpdate()
        {
            var commandBuffer = m_commandBuffer.CreateCommandBuffer();
            Entities.WithAll<DespawningEntity>().ForEach((Entity e, ref FoodTips tip) =>
            {
                if (EntityManager.Exists(tip.ui))
                {
                    commandBuffer.AddComponent<DespawningEntity>(tip.ui);
                }
            }).WithoutBurst().WithStructuralChanges().Run();
        }
    }
}