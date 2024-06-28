using System;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using Fibers;
using GameTools;
using GameTools.Paths;
using log4net;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace SGame
{
    /// <summary>
    /// 宠物逻辑, 跟随目标
    /// </summary>
    public class PetLogic : MonoBehaviour
    {
        private static ILog log = LogManager.GetLogger("xl.pet");
        private Transform   m_followTarget;
        private float       m_radius;
        private Transform   m_transform;
        private float       m_speed = 1.0f;

        private static PetLogic s_instance = null;

        public static PetLogic Instance => s_instance;

        private const int HALO_EFFECT_ID = 39; // 光环特效ID
            
        private static ConfigValueFloat PET_START_ANGLE     = new ConfigValueFloat("pet_start_angle", 20);
        private static ConfigValueFloat PET_START_DISTANCE  = new ConfigValueFloat("pet_start_distance", 2);
        private static ConfigValueFloat PET_TAKETIPS_TIME   = new ConfigValueFloat("pet_taketips_time", 3); // 获取小费配置
        private static ConfigValueFloat PET_RANDOMMOVE_INTERVAL  = new ConfigValueFloat("pet_randommove_interval", 3); // 获取小费配置
        private static ConfigValueFloat PET_TAKETIP_SPPED     = new ConfigValueFloat("pet_taketip_speed", 20);
        

        private Animator    m_animator;
        private static int WALK_NAME = 0;
        private Fiber       m_fiber;
        private Entity      m_entity = Entity.Null; // 代理对象, 用于
        private EntityManager EntityManager;

        private Entity m_haloEntity = Entity.Null; // 光环特效
        private GameConfigs.PetsRowData m_config;

        void Start()
        {
            if (WALK_NAME == 0)
                WALK_NAME = Animator.StringToHash("walk");
            m_animator = GetComponent<Animator>();
        }

        private void OnDestroy()
        {
            if (m_entity != Entity.Null)
            {
                EntityManager.DestroyEntity(m_entity);
                m_entity = Entity.Null;
            }

            if (m_haloEntity != Entity.Null)
            {
                EffectSystem.Instance.CloseEffect(m_haloEntity);
                m_haloEntity = Entity.Null;
            }
        }

        public void Initalzie(int petID, Transform follow, float radius, float speed, float scale)
        {
            s_instance = this;
            EntityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            ConfigSystem.Instance.TryGet(petID,  out m_config);
            m_followTarget  = follow;
            m_radius        = radius;
            m_transform     = transform;
            m_speed         =  speed;

            var rot2 = Quaternion.Euler(0, PET_START_ANGLE.Value, 0); // 绕Y轴旋转20度
            var dir = rot2 * m_followTarget.rotation * Vector3.forward;
            dir.y = 0;
            var offset = -dir.normalized * PET_START_DISTANCE.Value;

            m_transform.position = follow.position + offset;
            m_transform.rotation = Quaternion.identity;
            m_transform.localScale = new Vector3(scale, scale, scale);

            LoadEffect();

            m_entity = EntityManager.CreateEntity(typeof(Follow),
                typeof(LocalToWorld),
                typeof(Rotation),
                typeof(Translation),
                typeof(Speed),
                typeof(RotationSpeed),
                typeof(PathPositions),
                typeof(GameObjectSyncTag));
            
            EntityManager.AddComponentObject(m_entity, transform);
            EntityManager.SetComponentData(m_entity, new Speed(){Value = m_speed});
            EntityManager.SetComponentData(m_entity, new RotationSpeed(){Value = 20.0f});
            EntityManager.SetComponentData(m_entity, new Rotation(){Value = m_transform.rotation});
            EntityManager.SetComponentData(m_entity, new Translation(){Value = transform.position});

            m_fiber = new Fiber(Logic(), FiberBucket.Manual);
        }

        public void LoadEffect() 
        {
            if (m_config.ShowEffect > 0)
            {
                EffectSystem.Instance.Spawn3d(m_config.ShowEffect, null, m_transform.position);
            }
        }

        public void StopWalkAnim() 
        {
            m_animator.SetBool(WALK_NAME, false);
        }

        IEnumerator Logic()
        {
            // 创建光环 
            if (m_config.FootEffect > 0)
            {
                m_haloEntity = EffectSystem.Instance.Spawn3d(m_config.FootEffect, gameObject, Vector3.zero);
            }

            if (m_config.DelayMove > 0)
            {
                yield return new Fiber.Wait(m_config.DelayMove);
            }
            
            while (true)
            {
                yield return RandomMove(PET_TAKETIPS_TIME.Value);
                yield return TakeTips();
            }

        }
        

        // 从0-7, 8个数返回偏移位置
        static Vector2Int GetIndexOffset(int index)
        {
            switch (index)
            {
                case 0:
                    return new Vector2Int(-1, -1);
                case 1:
                    return new Vector2Int(0, -1);
                case 2:
                    return new Vector2Int(1, -1);
                case 3:
                    return new Vector2Int(-1, 0);
                case 4:
                    return new Vector2Int(1, 0);
                case 5:
                    return new Vector2Int(-1, 1);
                case 6:
                    return new Vector2Int(0, 1);
                case 7:
                    return new Vector2Int(1, 1);
            }

            throw new Exception("error index");
        }

        /// <summary>
        /// 通过玩家位置返回周围可移动的位置
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="map_pos"></param>
        /// <returns></returns>
        bool GetRandomWalkable(Vector3 pos, out Vector2Int map_pos, out Vector2Int dir)
        {
            Vector2Int centerGrid = MapAgent.VectorToGrid(pos);
            map_pos = Vector2Int.zero;
            dir = Vector2Int.zero;
            int startIndex = RandomSystem.Instance.NextInt(0, 8);
            for (int i = 0; i < 8; i++)
            {
                int index = (startIndex + i) % 8;
                dir = GetIndexOffset(index);

                map_pos = centerGrid + dir;
                int mapIndex = MapAgent.XYToCellIndex(map_pos.x, map_pos.y);
                if (MapAgent.agent.GetWalkable(mapIndex))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 随机移动到跟随对象附近
        /// </summary>
        /// <returns></returns>
        IEnumerator FollowCharacter(float waitTime)
        {
            if (waitTime > 0)
            {
                // 等待时间移动 
                yield return FiberHelper.Wait(waitTime);
            }
            
            Vector2Int map_pos;
            Vector2Int map_dir;
            if (m_followTarget == null)
                yield break;
            
            if (!GetRandomWalkable(m_followTarget.position, out map_pos, out map_dir))
            {
                log.Error("walkable pos not found!");
                yield break;
            }
            
            // 圈外跟随
            m_animator.SetBool(WALK_NAME, true);
            float3 curPos = EntityManager.GetComponentData<Translation>(m_entity).Value;

            Vector2Int curMapPos =  MapAgent.VectorToGrid(curPos);
            SetFindPath(Utils.GetAStarPosFromMapPos(curMapPos), Utils.GetAStarPosFromMapPos(map_pos));

            m_animator.SetBool(WALK_NAME, true);
            while (IsMoving() == true)
                yield return null;

            m_animator.SetBool(WALK_NAME, false);
        }

        void SetFindPath(int2 startPos, int2 endPos)
        {
            if (EntityManager.HasComponent<FindPathParams>(m_entity))
            {
                EntityManager.SetComponentData(m_entity, new FindPathParams(){start_pos = startPos, end_pos = endPos});
            }
            else
            {
                EntityManager.AddComponentData(m_entity, new FindPathParams() { start_pos = startPos, end_pos = endPos });
            }
        }

        public void Stop()
        {
            if (EntityManager.HasComponent<FindPathParams>(m_entity))
            {
                EntityManager.RemoveComponent<FindPathParams>(m_entity);
            }
            
            EntityManager.SetComponentData(m_entity, new Follow(){Value = 0});
        }

        /// <summary>
        /// 随机移动 
        /// </summary>
        /// <returns></returns>
        IEnumerator RandomMove(float runTime)
        {
            Fiber fiberFollow = new Fiber(FollowCharacter(0), FiberBucket.Manual);
            
            // 圈内不移动 
            while (runTime > 0)
            {
                runTime -= Time.deltaTime;
                bool inStep = fiberFollow.Step();
                if (!IsMoving())
                {
                    if (Vector3.Distance(m_followTarget.position, m_transform.position) > m_radius)
                    {
                        fiberFollow.Start(FollowCharacter(0));
                    }
                    else
                    {
                        if (!inStep)
                        {
                            fiberFollow.Start(FollowCharacter(PET_RANDOMMOVE_INTERVAL.Value));
                        }
                    }
                }
                yield return null;
            }
        }

        private float speed
        {
            set
            {
                EntityManager.SetComponentData(m_entity, new Speed(){Value = m_speed});
            }
        }

        /// <summary>
        /// 获取小费
        /// </summary>
        /// <returns></returns>
        IEnumerator TakeTips()
        {
            List<TableData> tables = new List<TableData>();

                
            // 1.获取小费的工作台
            List<TableData> allTables = TableManager.Instance.Datas;
            foreach (var t in allTables)
            {
                if (t.foodTip != Entity.Null && EntityManager.Exists(t.foodTip))
                {
                    tables.Add(t);
                }
            }
            if (tables.Count == 0)
                yield break;

            // 获得最近的区域
            int GetNearstTable(List<TableData> t, int2 pos, int count)
            {
                int index = 0;
                int distance = Utils.GetInt2DistanceFast(pos, t[0].map_pos);
                int2 lastPos = t[0].map_pos;
                for (int i = 1; i < count; i++)
                {
                    int newDistance = Utils.GetInt2DistanceFast(t[i].map_pos, pos);
                    if (newDistance < distance)
                    {
                        index = i;
                        distance = newDistance;
                    }
                }
                return index;
            }

            // 通过交换位置删除元素
            bool RemoveAndSwap(List<TableData> t, int removeIndex, int count)
            {
                TableData tmp = t[removeIndex];
                t[removeIndex] = t[count - 1];
                t[count - 1] = null;
                return true;
            }

            // 找到最近的点获取小费
            m_animator.SetBool(WALK_NAME, true);
            speed = PET_TAKETIP_SPPED.Value;
            for (int i = 0; i < tables.Count; i++)
            {
                float3      pos = EntityManager.GetComponentData<Translation>(m_entity).Value;
                Vector2Int v2Pos = MapAgent.VectorToGrid(pos);
                int2 currentPos = new int2(v2Pos.x, v2Pos.y);

                int itemCount = tables.Count - i;
                // 找到最近的桌子并移动
                int index = GetNearstTable(tables, currentPos, itemCount);
                yield return GoAndTakeTips(currentPos, tables[index]);
                RemoveAndSwap(tables, index, itemCount);
            }
            speed = m_speed;
            //EntityManager.DestroyEntity(m_entity);
            //m_entity = Entity.Null;
        }

        /// <summary>
        /// 去到目标座子上领取小费
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        IEnumerator GoAndTakeTips(int2 curPos, TableData target)
        {
            if (target.foodTip == Entity.Null || !EntityManager.Exists(target.foodTip))
            {
                target.foodTip = Entity.Null;
                yield break;
            }
            
            // 调用AStar寻路查询
            ChairData chair = target.GetFirstChair(CHAIR_TYPE.ORDER);
            var searchCurPos = MapAgent.GridToIndex(new Vector2Int(curPos.x, curPos.y));
            var searchEndPos = MapAgent.GridToIndex(new Vector2Int(chair.map_pos.x, chair.map_pos.y));
            EntityManager.AddComponentData(m_entity, new FindPathParams() { 
                start_pos = new int2(searchCurPos.x, searchCurPos.y), 
                end_pos = new int2(searchEndPos.x, searchEndPos.y) }
            );
            
            // 等待移动解锁
            while (IsMoving())
                yield return null;
            
            log.Info("Pet Logic GoAndTakeTips Step");
            EventManager.Instance.Trigger((int)GameEvent.FOOD_TIP_CLICK, target.foodTip);
            target.foodTip = Entity.Null;
            yield return null;
        }

        bool IsMoving()
        {
            if (EntityManager.HasComponent<FindPathParams>(m_entity))
                return true;

            if (EntityManager.HasComponent<Follow>(m_entity))
            {
                var follow = EntityManager.GetComponentData<Follow>(m_entity);
                return follow.Value > 0;
            }

            return false;
        }

        // Update is called once per frame
        void Update()
        {
            // 目标已经销毁, 自动删除
            if (m_followTarget == null)
            {
                if (m_fiber != null)
                {
                    m_fiber.Terminate();
                    m_fiber = null;
                }
                Destroy(gameObject);
                return;
            }

            if (m_fiber != null)
            {
                m_fiber.Step();
            }


        }
    }
}
