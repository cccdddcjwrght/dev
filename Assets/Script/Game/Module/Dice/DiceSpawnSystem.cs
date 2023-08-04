using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace SGame
{
    // 骰子生成器
    [DisableAutoCreation]
    public partial class DiceSpawnSystem : SystemBase
    {
        // 初始化标记
        public struct Initalized : IComponentData
        {
        }
        
        // 骰子的显示对象
        private GameObject        m_prefabDice;
        private ResourceManager   m_resourceManager ;
        private GameWorld         m_world;
        private const string      DICE_PATH = "Assets/BuildAsset/Prefabs/dice/client_dice.prefab";
        
        public void Initalize(GameWorld world, ResourceManager resourceManager)
        {
            m_world           = world;
            m_resourceManager = resourceManager;
            m_prefabDice      = resourceManager.LoadPrefab(DICE_PATH);
        }

        protected override void OnUpdate()
        {
            // DiceData.
            Entities.WithNone<Initalized>().ForEach((Entity e, ref DiceData dice,  in Translation trans, in Rotation rot) =>
            {
                EntityManager.AddComponent<Initalized>(e);
            }).WithStructuralChanges().WithoutBurst().Run();
        }
    }
}