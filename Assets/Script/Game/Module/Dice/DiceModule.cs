
namespace SGame
{
    public class DiceModule
    {
        private DiceSpawnSystem m_spawnSystem;
        private GameWorld       m_world;
        
        public DiceModule(GameWorld world, ResourceManager resourceManager)
        {
            m_spawnSystem = world.GetECSWorld().CreateSystem<DiceSpawnSystem>();
            m_spawnSystem.Initalize(world, resourceManager);
        }

        public void Update()
        {
            m_spawnSystem.Update();
        }

        public void Shutdown()
        {
            m_world.GetECSWorld().DestroySystem(m_spawnSystem);
        }
    }
}