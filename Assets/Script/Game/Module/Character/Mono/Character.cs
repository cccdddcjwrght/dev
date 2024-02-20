using System;
using GameTools;
using GameTools.Paths;
using log4net;
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
        private const string DISH_OFFSET_NAME = "dish_offsety"; // 放餐偏移
        
        /// <summary>
        /// 脚本数据
        /// </summary>
        public GameObject script;

        /// <summary>
        /// 模型数据
        /// </summary>
        public GameObject model;

        public Animator modelAnimator;

        /// <summary>
        /// Entity对象
        /// </summary>
        public Entity entity;

        /// <summary>
        /// 食物Enitty
        /// </summary>
        public Entity m_food { get; private set; }
        
        /// <summary>
        /// uiEnitty
        /// </summary>
        public Entity m_hud { get; private set; }

        /// <summary>
        /// 角色实例化ID
        /// </summary>
        public int CharacterID = 0;

        /// <summary>
        /// 角色类型
        /// </summary>
        public int roleType = 0;

        public int roleID = 0;

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
            modelAnimator = model.GetComponent<Animator>();

            // 触发初始化角色事件
            EventBus.Trigger(CharacterInit.EventHook, script, this);
        }

        /// <summary>
        /// 获取角色挂点
        /// </summary>
        /// <param name="slotType"></param>
        /// <returns></returns>
        public Transform GetAttachementPoint(SlotType slotType)
        {
            return this.transform;
        }

        /// <summary>
        /// 设置角色移动完最后的朝向
        /// </summary>
        /// <param name="rot">旋转角度0-360, 负数表示没有</param>
        public void LastRotation(float rot)
        {
            if (rot < 0)
            {
                entityManager.HasComponent<LastRotation>(entity);
                entityManager.RemoveComponent<LastRotation>(entity);
                return;
            }

            if (!entityManager.HasComponent<LastRotation>(entity))
            {
                entityManager.AddComponent<LastRotation>(entity);
            }
            
            //float2 dir = new float2(math.cos(rot * Mathf.Deg2Rad), math.sin(Mathf.Deg2Rad * rot));
            // 绕Y轴旋转
            quaternion value = quaternion.AxisAngle(new float3(0, 1, 0), rot * Mathf.Deg2Rad);
            //quaternion value = quaternion.LookRotation(new float3(dir.x, 0, dir.y), new float3(0, 1, 0));
            entityManager.SetComponentData(entity, new LastRotation() { Value = value });
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
            entityManager.SetComponentData(food, new Translation() {Value = new float3(0, 0.5f, 0.5f)});
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

                float posY = GameConfigs.GlobalDesginConfig.GetFloat(DISH_OFFSET_NAME);
                
                worldPos += new float3(0, posY, 0);
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

        /// <summary>
        /// 存储当前hud
        /// </summary>
        /// <param name="hud"></param>
        public void AddHudEntity(Entity hud)
        {
            m_hud =hud;
        }

        /// <summary>
        /// 关闭当前hud
        /// </summary>
        public void ClearHudEntity()
        {
            if (m_hud != Entity.Null)
            {
                UIUtils.CloseUI(m_hud);
                m_hud = Entity.Null;
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