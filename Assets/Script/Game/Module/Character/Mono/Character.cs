using System.Collections;
using System.Collections.Generic;
using GameTools.Paths;
using log4net;
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
        private static ILog log = LogManager.GetLogger("game.character");
        
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
        /// 食物Enitty
        /// </summary>
        public Entity m_food { get; private set; }

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
        public Entity CreateFood(int foodType)
        {
            var req = entityManager.CreateEntity(typeof(SpawnFoodRequestTag));
            var q1 = quaternion.Euler(-math.PI / 2, 0, 0, math.RotationOrder.ZXY);

            m_food = FoodModule.Instance.CreateFood(foodType, new float3(0, 0.5f, 0.5f), quaternion.identity);
            AddChild(m_food);
            return m_food;
        }

        // 添加子节点
        public void AddChild(Entity e)
        {
            Utils.AddEntityChild(entity, e);
        }
        
        // 删除子节点
        private void RemoveChild(Entity e)
        {
            Utils.RemoveEntityChild(entity, e);
        }
        
        /// <summary>
        /// 拿取食物
        /// </summary>
        /// <param name="food"></param>
        public void TakeFood(Entity food)
        {
            // 设置父节点为自己
            entityManager.SetComponentData(food, new Translation() {Value = new float3(0, 0.5f, 0.1f)});
            entityManager.SetComponentData(food, new Rotation(){Value = quaternion.identity});
            AddChild(food);
            this.m_food = food;
        }

        
        /// <summary>
        /// 放置食物到目标位置上去
        /// </summary>
        /// <param name="pos"></param>
        public Entity PlaceFoodToTable(int2 pos)
        {
            Entity old = m_food;
            if (m_food != Entity.Null && entityManager.Exists(m_food))
            {
                RemoveChild(m_food);
                Vector3 postemp = GameTools.MapAgent.CellToVector(pos.x, pos.y);
                float3 worldPos = postemp;
                worldPos += new float3(0, 2, 0);
                entityManager.SetComponentData(m_food, new Translation() { Value = worldPos });
                entityManager.SetComponentData(m_food, new Rotation() { Value = quaternion.identity });
            }
            else
            {
                log.Error("FOOD IS EMPTY!!!");
            }
            m_food = Entity.Null;
            return old;
        }

        /// <summary>
        /// 清除手上的食物
        /// </summary>
        /// <param name="foodType"></param>
        public void ClearFood()
        {
            if (m_food != Entity.Null)
            {
                RemoveChild(m_food);
                FoodModule.Instance.CloseFood(m_food);
                m_food = Entity.Null;
            }
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