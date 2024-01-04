using FairyGUI;
using Unity.Entities;
using SGame.UI.Hud;
using log4net;

namespace SGame
{
    [UpdateAfter(typeof(FloatTextSystem))]
    [UpdateInGroup(typeof(GameUIGroup))]
    public partial class DespawnFloatTextSystem : SystemBase
    {
        private EntityArchetype                                 m_floatTextType;
        public ObjectPool<UI_FloatText>                         m_floatComponents;
        private Entity                                          m_hud;
        private GComponent                                      m_hudContent;
        private EndInitializationEntityCommandBufferSystem      m_commandBuffer;
        private static ILog log = LogManager.GetLogger("xl.game.floatext");

        public void Initalize(Entity hud, ObjectPool<UI_FloatText> pool)
        {
            m_hud               = hud;
            m_floatComponents   = pool;
        }

        protected override void OnCreate()
        {
            base.OnCreate();
            m_commandBuffer = World.GetOrCreateSystem<EndInitializationEntityCommandBufferSystem>();
        }

        protected override void OnUpdate()
        {
            var commandBuffer = m_commandBuffer.CreateCommandBuffer();
            Entities.WithAll<DespawningEntity>().ForEach((Entity e, in FloatTextData data) =>
            {
                log.Info("free id=" + data.Value.ToString());
                m_floatComponents.Free(data.Value);
            }).WithoutBurst().Run();
        }
    }
}