using SGame.UI;
using Unity.Entities;

namespace SGame
{
    public class HudModule : IModule
    {
        private GameWorld m_gameWorld;
        private Entity    m_hudUI;
        //private SystemCollection m_systems;
        
        public HudModule(GameWorld world)
        {
            m_gameWorld                 = world;
            //m_systems                   = new SystemCollection();
            //var spawnFloatTextSystem    = World.CreateSystem<SpawnFloatTextSystem>();
            //var floatTextSystem         = World.CreateSystem<FloatTextSystem>();
            //var despawnFloatTextSystem  = World.CreateSystem<DespawnFloatTextSystem>();
            
            //m_systems.Add(spawnFloatTextSystem);
            //m_systems.Add(floatTextSystem);
            //m_systems.Add(despawnFloatTextSystem);
            
            // 创建UI
            m_hudUI = UIRequest.Create(EntityManager, UIUtils.GetUI("hud"));
            //spawnFloatTextSystem    .Initalize(m_hudUI);
            //floatTextSystem         .Initalize(m_hudUI, spawnFloatTextSystem.pool);
            //despawnFloatTextSystem  .Initalize(m_hudUI, spawnFloatTextSystem.pool);
        }

        EntityManager EntityManager
        {
            get
            {
                return m_gameWorld.GetEntityManager();
            }
        }

        World World
        {
            get
            {
                return m_gameWorld.GetECSWorld();
            }
        }

        public void Update()
        {
            //m_systems.Update();
        }

        public void Shutdown()
        {
            //m_systems.Shutdown(World);
        }
    }
}