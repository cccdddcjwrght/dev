
namespace SGame
{
    public class DiceModule
    {
        private SystemCollection m_collection;
        private GameWorld       m_world;
        
        public DiceModule(GameWorld world, ResourceManager resourceManager)
        {
            m_collection = new SystemCollection();
            var spawnSystem = world.GetECSWorld().CreateSystem<DiceSpawnSystem>();
            var animSystem = world.GetECSWorld().CreateSystem<DiceAnimationSystem>();
            var despawnSystem = world.GetECSWorld().CreateSystem<DespawnDiceSystem>();
            spawnSystem.Initalize(world, resourceManager);
            animSystem.Initalize(world, resourceManager);
            //despawnSystem.Initalize(world, resourceManager);

            m_collection.Add(spawnSystem);
            m_collection.Add(animSystem);
            m_collection.Add(despawnSystem);
        }

        public void Update()
        {
            m_collection.Update();
        }

        public void Shutdown()
        {
            m_collection.Shutdown(m_world.GetECSWorld());
        }
    }
}