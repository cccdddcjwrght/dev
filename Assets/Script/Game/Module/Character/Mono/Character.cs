using System.Collections;
using System.Collections.Generic;
using GameTools.Paths;
using OfficeOpenXml.Style;
using UnityEngine;
using Unity.Entities;
using Unity.VisualScripting;
using SGame.VS;
using Unity.Mathematics;
using Unity.Transforms;

namespace SGame
{
    /// <summary>
    /// 角色数据处理
    /// </summary>
    public class Character : MonoBehaviour
    {
        /// <summary>
        /// 脚本数据
        /// </summary>
        public GameObject script;

        /// <summary>
        /// 模型数据
        /// </summary>
        public GameObject model;

        /// <summary>
        /// Entity对象
        /// </summary>
        public Entity entity;

        /// <summary>
        /// 角色实例化ID
        /// </summary>
        public int CharacterID = 0;

        /// <summary>
        /// 角色类型
        /// </summary>
        public int roleType = 0;

        public Transform pos
        {
            get { return transform; }
        }

        private EntityManager entityManager;
        
        public EnumTarget GetTargetType()  { return Utils.GetTargetFromRoleType(roleType);  }
        
        /// <summary>
        /// 初始化角色
        /// </summary>
        /// <param name="entity">角色的ENTITY</param>
        /// <param name="mgr">Entity管理器</param>
        public void OnInitCharacter(Entity entity, EntityManager mgr)
        {
            this.entity = entity;
            this.entityManager = mgr;

            // 触发初始化角色事件
            EventBus.Trigger(CharacterInit.EventHook, script, this);
        }
        

        /// <summary>
        /// 显示等待HUD
        /// </summary>
        /// <param name="progressTime"></param>
        /// <returns></returns>
        public Entity ShowWaitUI(float progressTime)
        {
            Entity ui = UIUtils.ShowHUD("progress", transform, float3.zero);
            entityManager.AddComponent<LiveTime>(ui);
            entityManager.SetComponentData(ui, new LiveTime() {Value =  progressTime});
            return ui;
        }
        

        /// <summary>
        /// 角色移动到目标位置
        /// </summary>
        /// <param name="map_pos"></param>
        public void MoveTo(int2 map_pos)
        {
            // Debug.Log("you move to =" + map_pos.ToString());
            var searchPos = GameTools.MapAgent.GridToIndex(new Vector2Int(map_pos.x, map_pos.y));
            map_pos.x = searchPos.x;
            map_pos.y = searchPos.y;
            float3 pos = entityManager.GetComponentData<Translation>(entity).Value;
            int2 curPos = AStar.GetGridPos(pos);
            
            FindPathParams find = new FindPathParams() { start_pos = curPos, end_pos = map_pos };
            if (!entityManager.HasComponent<FindPathParams>(entity))
            {
                entityManager.AddComponent<FindPathParams>(entity);
            }
            
            entityManager.SetComponentData(entity, find);
        }

        /// <summary>
        /// 创建食物
        /// </summary>
        /// <param name="foodType"></param>
        public void CreateFood(int foodType)
        {
            var req = entityManager.CreateEntity(typeof(SpawnFoodRequest));
            var q1 = quaternion.Euler(-math.PI / 2, 0, 0, math.RotationOrder.ZXY);

            entityManager.SetComponentData(req, new SpawnFoodRequest()
            {
                foodType = foodType,
                parent = entity,
                pos = new float3(0, 0.5f, 0.5f),
                rot = quaternion.Euler(-90, 0, 0),
                isShow = false,
            });
        }

        /// <summary>
        /// 显示食物, 删除Disable节点
        /// </summary>
        public Entity ShowFood()
        {
            var holder = entityManager.GetComponentData<FoodHolder>(entity);
            if (holder.Value != Entity.Null && entityManager.Exists(holder.Value))
            {
                if (entityManager.HasComponent<Disabled>(holder.Value))
                {
                    entityManager.RemoveComponent<Disabled>(holder.Value);
                }
                return holder.Value;
            }

            return Entity.Null;
        }
        
        /// <summary>
        /// 拿取食物
        /// </summary>
        /// <param name="food"></param>
        public void TakeFood(Entity food)
        {
            // 设置父节点为自己
            if (!entityManager.HasComponent<Parent>(food))
            {
                entityManager.AddComponent<Parent>(food);
            }
            DynamicBuffer<Child> childs;
            if (!entityManager.HasComponent<Child>(entity))
            {
                childs = entityManager.AddBuffer<Child>(entity);
            }
            else
            {
                childs = entityManager.GetBuffer<Child>(entity);
            }
            childs.Add(new Child() { Value = food });
            entityManager.SetComponentData(food, new Parent() { Value = entity});
            entityManager.SetComponentData(food, new Translation() {Value = new float3(0, 0.5f, 0.1f)});
            
            // 持有食物
            entityManager.SetComponentData(entity, new FoodHolder{ Value = food});
        }

        private bool RemoveChild(Entity e)
        {
            if (entityManager.HasComponent<Child>(entity))
            {
                var buff = entityManager.GetBuffer<Child>(entity);
                for (int i = 0; i < buff.Length; i++)
                {
                    if (buff[i].Value == e)
                    {
                        buff.RemoveAtSwapBack(i);
                        return true;
                    }
                }
            }

            return false;
        }
        
        
        /// <summary>
        /// 放置食物到目标位置上去
        /// </summary>
        /// <param name="pos"></param>
        public void PlaceFoodToTable(int2 pos)
        {
            float3 worldPos = GameTools.MapAgent.CellToVector(pos.x, pos.y);
            worldPos += new float3(0, 0, 1);
            var holder = entityManager.GetComponentData<FoodHolder>(entity);
            if (holder.Value != Entity.Null && entityManager.Exists(holder.Value))
            {
                RemoveChild(holder.Value);
                entityManager.SetComponentData<Parent>(holder.Value, new Parent(){Value = Entity.Null});
                entityManager.SetComponentData(holder.Value, new Translation(){Value = worldPos});
                entityManager.SetComponentData(entity, new FoodHolder(){Value = Entity.Null});
            }
        }

        /// <summary>
        /// 清除手上的食物
        /// </summary>
        /// <param name="foodType"></param>
        public void ClearFood()
        {
            var holder = entityManager.GetComponentData<FoodHolder>(entity);
            if (holder.Value != Entity.Null)
            {
                if (entityManager.Exists(holder.Value))
                {
                    World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<DespawnEntitySystem>().DespawnEntity(holder.Value);
                }
                
                RemoveChild(holder.Value);
                entityManager.SetComponentData(entity, new FoodHolder() { Value = Entity.Null });
            }
        }

        /// <summary>
        /// 获取食物
        /// </summary>
        /// <returns></returns>
        public Entity GetFood()
        {
            var holder = entityManager.GetComponentData<FoodHolder>(entity);
            return holder.Value;
        }

        public bool isMoving
        {
            get
            {
                if (entityManager.HasComponent<FindPathParams>(entity))
                    return true;
                
                if (entityManager.HasComponent<PathPositions>(entity) == false)
                    return false;

                if (!entityManager.HasComponent<Follow>(entity))
                    return false;

                var follow = entityManager.GetComponentData<Follow>(entity);
                return follow.Value > 0;
            }
        }
    }
}