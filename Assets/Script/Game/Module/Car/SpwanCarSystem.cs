using System;
using System.Collections;
using System.Collections.Generic;
using GameConfigs;
using libx;
using log4net;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.VisualScripting;
using Unity.Mathematics;

namespace SGame
{
    [UpdateInGroup(typeof(GameLogicGroup))]
    public class SpawnCarSystem : ComponentSystem
    {
        private static ILog log = LogManager.GetLogger("game.car");

        private EntityArchetype m_carArchetype;
        private AssetRequest    m_baseCar;
        
        public class SpawnRequest : IComponentData
        {
            // 地图2D 位置
            public float3 pos;
        
            // 旋转角度
            public quaternion rot;

            // ID
            public int id;

            public SpawnResult result;
            
            public static Entity Create(int id, float3 pos, float angle)
            {
                var mgr = World.DefaultGameObjectInjectionWorld.EntityManager;
                var entity = mgr.CreateEntity();
                var req = new SpawnRequest()
                {
                    id = id,
                    pos = pos,
                    rot = quaternion.AxisAngle(new float3(0,1.0f,0), angle * math.PI / 180),
                    result = new SpawnResult() { entity = Entity.Null }
                };
                mgr.AddComponentObject(entity, req);
                return entity;
            }
        }
        
        protected override void OnCreate()
        {
            m_carArchetype = EntityManager.CreateArchetype(
                typeof(Speed), 
                typeof(Translation), 
                typeof(Rotation),
                typeof(LocalToWorld),
                typeof(RotationSpeed),
                typeof(Follow),
                typeof(GameObjectSyncTag),
                typeof(FPathPositions),
                typeof(CarData),
                typeof(CarCustomer)
                );

            m_baseCar = Assets.LoadAssetAsync("Assets/BuildAsset/Prefabs/Car/CarRoot.prefab", typeof(GameObject));
        }

        protected override void OnStartRunning()
        {
        }
        

        public void Clear()
        {
            
        }

        Entity SetupGameObject(SpawnRequest req)
        {
            var obj = GameObject.Instantiate(m_baseCar.asset as GameObject);
            CarMono carscript = obj.GetComponent<CarMono>();

            var entity            = EntityManager.CreateEntity(m_carArchetype);
            var rot     = req.rot;
            var pos         = req.pos;
            
            obj.transform.rotation = rot;
            obj.transform.position = pos;
            EntityManager.AddComponentObject(entity, obj.transform);
            EntityManager.AddComponentObject(entity, carscript);
            EntityManager.SetComponentData(entity, new Translation(){Value = pos});
            EntityManager.SetComponentData(entity, new Rotation(){Value = rot});
            EntityManager.SetComponentData(entity, new RotationSpeed(){Value =  10.0f});
            EntityManager.SetComponentData(entity, new CarData(){id = req.id});
            if (!carscript.Initalize(EntityManager, entity, req.id))
                EntityManager.AddComponent<DespawningTag>(entity);
            return entity;
        }

        protected override void OnUpdate()
        {
            if (!m_baseCar.isDone)
                return;
            
            // 获取数据
            Entities.ForEach((Entity e,SpawnRequest req) =>
            {
                if (EntityManager.HasComponent<DespawningTag>(e))
                {
                    req.result.error = "already close";
                    EntityManager.DestroyEntity(e);
                    return;
                }
                
                req.result.entity = SetupGameObject(req);
                
                // 删除请求对象
                EntityManager.DestroyEntity(e);
            });
        }
    }
}