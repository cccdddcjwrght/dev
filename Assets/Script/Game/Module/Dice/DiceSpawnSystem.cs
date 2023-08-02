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
                //EntityManager.AddComponent<EntitySyncGameObject>(e);
                
                // 生成位置
                //Vector3           pos = trans.Value;
                //Quaternion quaternion = rot.Value;
                //m_world.SpawnInternal(m_prefabDice, pos, quaternion, out dice.m_instance);

                // 挂接同步脚本
                 /*
                EntityManager.SetComponentData(e, new EntitySyncGameObject() { Value = dice.m_instance });
                if (!EntityManager.HasComponent<LinkedEntityGroup>(e))
                {
                    EntityManager.AddBuffer<LinkedEntityGroup>(e);
                }
                
                // 关联对象
                DynamicBuffer<LinkedEntityGroup> group = EntityManager.GetBuffer<LinkedEntityGroup>(e);
                group.Add(new LinkedEntityGroup() { Value = dice.m_instance });
                */
            }).WithStructuralChanges().WithoutBurst().Run();
        }
    }
}