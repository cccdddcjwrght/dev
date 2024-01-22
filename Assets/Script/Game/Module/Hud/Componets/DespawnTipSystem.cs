using FairyGUI;
using Unity.Entities;
using SGame.UI.Hud;
using log4net;

namespace SGame
{
    [UpdateInGroup(typeof(GameUIGroup))]
    public partial class DespawnTipSystem : SystemBase
    {
        private EntityArchetype                                 m_floatTextType;
        public  ObjectPool<UI_OrderTip>                         m_tipComponents = new ObjectPool<UI_OrderTip>();
        private Entity                                          m_hud;
        private GComponent                                      m_hudContent;
        private EndInitializationEntityCommandBufferSystem      m_commandBuffer;
        private static ILog log = LogManager.GetLogger("xl.game.tipClose");

        public void Initalize(Entity hud, ObjectPool<UI_OrderTip> pool)
        {
            m_hud               = hud;
            m_tipComponents     = pool;
        }

        protected override void OnCreate()
        {
            base.OnCreate();
            m_commandBuffer = World.GetOrCreateSystem<EndInitializationEntityCommandBufferSystem>();
        }

        protected override void OnUpdate()
        {
            var commandBuffer = m_commandBuffer.CreateCommandBuffer();
            // Entities.WithAll<DespawningEntity>().ForEach((Entity e, in TipData data) =>
            // {
            //     
            // }).WithoutBurst().Run();
        }
    }
}