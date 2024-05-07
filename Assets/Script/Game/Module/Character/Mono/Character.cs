    using System;
using System.Collections;
using Fibers;
using GameConfigs;
using GameTools;
using GameTools.Paths;
using log4net;
using UnityEngine;
using Unity.Entities;
using Unity.VisualScripting;
using SGame.VS;
using Unity.Mathematics;
using Unity.Transforms;
using System.Collections.Generic;

    
namespace SGame
{
    /// <summary>
    /// 角色数据处理
    /// </summary>
    public class Character : MonoBehaviour
    {
        private static ILog log = LogManager.GetLogger("game.character");
        private const string DISH_OFFSET_NAME = "dish_offsety"; // 放餐偏移
        private Fiber m_modelLoading;
        
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
        /// 待机效果
        /// </summary>
        public Entity m_sleep { get; private set; }

        /// <summary>
        /// uiEnitty
        /// </summary>
        public Entity m_hud { get; set; }
        
        public Entity m_effect{ get;  set; }

        /// <summary>
        /// 角色实例化ID
        /// </summary>
        public int CharacterID = 0;

        /// <summary>
        /// 角色类型
        /// </summary>
        public int roleType = 0;

        /// <summary>
        /// 角色属性配置表的ID
        /// </summary>
        public int roleID = 0;

        /// <summary>
        /// 是否是雇员
        /// </summary>
        public bool isEmployee => playerID != 0;

        /// <summary>
        /// 额外添加的速度, 用于好友通过脚本设置 
        /// </summary>
        public float  externSpeed = 0;

        /// <summary>
        /// 好友顾客ID, 每个玩家的唯一ID， 0表示没有
        /// </summary>
        public int playerID = 0;

        private bool isPlay=false;
        public Transform pos
        {
            get { return transform; }
        }

        private EntityManager entityManager;
        
        public EnumTarget GetTargetType()  { return Utils.GetTargetFromRoleType(roleType);  }

        private Equipments m_slot;

        private string     m_characterLooking;

        void Awake()
        {
            m_modelLoading = new Fiber(FiberBucket.Manual);
        }
        
        void Start()
        {
            m_slot = gameObject.AddComponent<Equipments>();
        }

        void Update()
        {
            if (m_modelLoading != null && !m_modelLoading.IsTerminated)
                m_modelLoading.Step();
        }

        /// <summary>
        /// 获取外观字符串
        /// </summary>
        /// <returns></returns>
        public string GetCurrentLooking()
        {
            if (!string.IsNullOrEmpty(m_characterLooking))
            {
                return m_characterLooking;
            }
            
            ConfigSystem.Instance.TryGet(roleID, out GameConfigs.RoleDataRowData roleData);
            ConfigSystem.Instance.TryGet(roleData.Model, out GameConfigs.roleRowData config);
            m_characterLooking = config.Part;
            return m_characterLooking;
        }

        private void OnDestroy()
        {
            ClearFood();
            ClearHudEntity();
        }

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

        public void CacheCharacter()
        {
            PriorityManager.Instance.AddRoleData(this);
        }

        /// <summary>
        /// 获得挂点
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public Transform GetSlot(SlotType t)
        {
            if (m_slot != null)
                return m_slot.GetSlot(t);

            return gameObject.transform;
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
                if (entityManager.HasComponent<LastRotation>(entity))
                    entityManager.RemoveComponent<LastRotation>(entity);
                return;
            }

            if (!entityManager.HasComponent<LastRotation>(entity))
            {
                entityManager.AddComponent<LastRotation>(entity);
            }
            
            // 绕Y轴旋转
            quaternion value = quaternion.AxisAngle(new float3(0, 1, 0), rot * Mathf.Deg2Rad);
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
        /// <param name="itemId">道具ID</param>
        public Entity CreateFood(int itemId)
        {
            if (!ConfigSystem.Instance.TryGet(itemId, out ItemRowData config))
            {
                log.Error("item not found =" + itemId);
                return Entity.Null;
            }

            m_food = EffectSystem.Instance.Spawn3d(config.ModelEffectID, GetSlot(SlotType.FOOD).gameObject);
            if (m_food == Entity.Null)
            {
                log.Error("item effect id not found=" + itemId.ToString());
            }
            
            return m_food;
        }
        

        /// <summary>
        /// 设置速度属性
        /// </summary>
        /// <param name="enable"></param>
        public void SetEnableSpeedAttribute(bool enable)
        {
            bool hasDisable = entityManager.HasComponent<DisableAttributeTag>(entity);
            if (enable == hasDisable)
            {
                if (enable)
                {
                    entityManager.RemoveComponent<DisableAttributeTag>(entity);
                }
                else
                {
                    entityManager.AddComponent<DisableAttributeTag>(entity);
                }
            }
        }

        /// <summary>
        /// 设置角色移动速度
        /// </summary>
        /// <param name="speed"></param>
        public void SetSpeed(float speed)
        {
            entityManager.SetComponentData(entity, new Speed() { Value = speed });
        }
        
        /// <summary>
        /// 拿取食物
        /// </summary>
        /// <param name="food"></param>
        public void TakeFood(Entity food)
        {
            // 设置父节点为自己
            //entityManager.SetComponentData(food, new Translation() {Value = new float3(0, 0.5f, 0.5f)});
            //entityManager.SetComponentData(food, new Rotation(){Value = quaternion.identity});
            //AddChild(food);
            Transform foodTransform = entityManager.GetComponentObject<Transform>(food);
            foodTransform.SetParent(GetSlot(SlotType.FOOD), false);
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
                Vector3 postemp = GameTools.MapAgent.CellToVector(pos.x, pos.y);
                float3 worldPos = postemp;
                float posY = ConstDefine.DISH_OFFSET_Y;
                worldPos += new float3(0, posY, 0);

                Transform foodTrans = entityManager.GetComponentObject<Transform>(m_food);
                foodTrans.SetParent(null);
                foodTrans.position = worldPos;
            }
            else
            {
                log.Error("FOOD IS EMPTY!!!" + pos);
            }
            m_food = Entity.Null;
            return old;
        }

        public int2 GetInt2Pos()
        {
            return AStar.GetGridPos(pos.position);
        }

        /// <summary>
        /// 清除手上的食物
        /// </summary>
        /// <param name="foodType"></param>
        public void ClearFood()
        {
            if (m_food != Entity.Null)
            {
                //RemoveChild(m_food);
                //FoodModule.Instance.CloseFood(m_food);
                EffectSystem.Instance.CloseEffect(m_food);
                m_food = Entity.Null;
            }
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
        
        public bool CheckEntityNull(Entity e)
        {
            if (e != Entity.Null)
            {
                return false;
            }

            return true;
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

        /// <summary>
        /// 整体更改外观
        /// </summary>
        /// <param name="part">完整的外观字符串</param>
        public void ChangeLooking(string part)
        {
            m_modelLoading.Start(ChangLooking(part));
        }

        /// <summary>
        /// 更新外观
        /// </summary>
        /// <param name="part">部分字符串</param>
        public void UpdateLooking(string part)
        {
            string oldLooking = GetCurrentLooking();
            var look1 = CharacterPartGen.ParseString(oldLooking);
            var look2 = CharacterPartGen.ParseString(part, false);
            look1.Merge(look2);

            ChangeLooking(look1.ToPartString());
        }

        /// <summary>
        /// 带武器的looking
        /// </summary>
        /// <param name="part"></param>
        /// <returns></returns>
        IEnumerator ChangLooking(string part)
        {
            m_characterLooking = part;
            var data = CharacterPartGen.ParseString(part);
            List<string> weapons = data.GetValues("weapon");
            List<string> effects = data.GetValues("effect");
            string pet = data.GetValue("pet");
            data.RemoveDatas("weapon");
            data.RemoveDatas("effect");
            data.RemoveData("pet");
            var newPart = data.ToPartString();
            var gen = CharacterGenerator.CreateWithConfig(newPart);
            while (gen.ConfigReady == false)
                yield return null;
			
            var ani = gen.Generate();


            ani.transform.SetParent(transform, false);
            ani.transform.localRotation = Quaternion.identity;
            ani.transform.localPosition = Vector3.zero;
            ani.transform.localScale = Vector3.one;
            ani.name = "Model";
            
            ConfigSystem.Instance.TryGet(roleID, out GameConfigs.RoleDataRowData roleData);
            ConfigSystem.Instance.TryGet(roleData.Model, out GameConfigs.roleRowData config);
            if (config.RoleScaleLength == 3)
            {
                var scaleVector = new Vector3(config.RoleScale(0), config.RoleScale(1), config.RoleScale(2));
                ani.transform.localScale = scaleVector;
            }

            GameObject.Destroy(model);
            yield return null;
            m_slot.Clear();
            m_slot.UpdateModel();
            model = ani;
            
            // 设置武器
            foreach (var weaponStr in weapons)
            {
                if (!int.TryParse(weaponStr, out int weaponID))
                {
                    log.Error("parse weapon id fail=" + weaponStr);
                }
                else
                {
                    yield return null;
                    m_slot.SetWeapon(weaponID);
                }
            }

            // 设置特效
            foreach (var effectStr in effects)
            {
                if (!int.TryParse(effectStr, out int effectID))
                {
                    log.Error("parse weapon id fail=" + effectStr);
                }
                else
                {
                    m_slot.SetEffect(effectID);
                }
            }
            
            // 设置宠物ID
            if (!string.IsNullOrEmpty(pet))
            {
                if (!int.TryParse(pet, out int petID))
                {
                    log.Error("parse pet id fail=" + pet);
                }
                else
                {
                    m_slot.SetPet(petID);
                }
            }

            modelAnimator = ani.GetComponent<Animator>();
        }

        public void ShowSleep() 
        {
            if(m_sleep == Entity.Null)
                m_sleep = EffectSystem.Instance.Spawn3d(8, gameObject);
        }

        public void ClearSleep() 
        {
            if (m_sleep != Entity.Null)
            {
                EffectSystem.Instance.CloseEffect(m_sleep);
                m_sleep = Entity.Null;
            }
        }

        /// <summary>
        /// 开始还是结束制作食物
        /// </summary>
        /// <param name="start"></param>
        public void Cooking(bool start)
        {
            if (modelAnimator != null)
            {
                modelAnimator.SetBool("working", start);
            }
        }
    }
}