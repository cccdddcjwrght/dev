using System.Data.SqlTypes;
using Unity.Entities;
namespace SGame
{
    public class DiceModule 
    {
        private SystemCollection  m_collection;
        private ResourceManager   m_resourceManager;
        private GameWorld         m_world;
        private const string DICE_ASSET_PATH = "Assets/BuildAsset/Prefabs/dice/dice.prefab";
        private Entity            m_prefab;

        private Entity            m_dice;
        
        private static DiceModule s_instance = null;

        public static DiceModule Instance
        {
            get
            {
                return s_instance;
            }
        }

        public DiceModule(GameWorld world, ResourceManager resourceManager)
        {
            s_instance = this;
            m_world = world;
            m_resourceManager = resourceManager;
            m_collection = new SystemCollection();
            
            // 创建洗头膏
            var spawnSystem = world.GetECSWorld().CreateSystem<DiceSpawnSystem>();
            
            // 动画系统
            var animSystem = world.GetECSWorld().CreateSystem<DiceAnimationSystem>();
            
            // 销毁系统
            var despawnSystem = world.GetECSWorld().CreateSystem<DespawnDiceSystem>();
            spawnSystem.Initalize(world, resourceManager);
            animSystem.Initalize(world, resourceManager);

            m_collection.Add(spawnSystem);
            m_collection.Add(animSystem);
            m_collection.Add(despawnSystem);

            m_prefab = resourceManager.GetEntityPrefab(DICE_ASSET_PATH);
            m_world.GetEntityManager().RemoveComponent<LinkedEntityGroup>(m_prefab);
            RunTest();
        }

        public Entity GetDice()
        {
            return m_dice;
        }

        void RunTest()
        {
            //m_resourceManager.SpawnAndCovert(DICE_ASSET_PATH);
            m_dice = m_world.GetEntityManager().Instantiate(m_prefab);
        }

        // 骰子移动 
        public void Play(Entity dice, float time, int value)
        {
            var manager = m_world.GetEntityManager();
            manager.SetComponentData(dice, new DiceData(){Value =  value});

            var animData = manager.GetComponentData<DiceAnimation>(dice);
            animData.time = time;
            animData.state = DiceAnimation.AnimState.PLAYING;
            manager.SetComponentData(dice, animData);
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