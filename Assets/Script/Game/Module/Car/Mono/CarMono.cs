using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Fibers;
using GameTools;
using UnityEngine;
using Unity.Entities;
using libx;
using log4net;
using Unity.Mathematics;

namespace SGame
{
    public class CarMono : MonoBehaviour
    {
        private Entity                      m_entity;   // 车的ENTITY      
        private GameConfigs.CarDataRowData  m_config;   // 配置表
        private GameObject                  m_ai;       // AI 脚本
        private GameObject                  m_model;    // 模型
        private Fiber                       m_logic;    // 逻辑运行
        
        private static ILog                 log = LogManager.GetLogger("game.car");
        private EntityManager               EntityManager;
        private const string                ASSET_PATH = "Assets/BuildAsset/Prefabs/";
        private List<Vector3>               m_roads;    // 路径点
        private CarQueue                    m_queue;    // 队伍

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="e"></param>
        /// <param name="config"></param>
        public bool Initalize(EntityManager entityManager, Entity e,  int id)
        {
            if (!ConfigSystem.Instance.TryGet(id, out GameConfigs.CarDataRowData config))
            {
                log.Error("car config not found=" + id);
                return false;
            }

            EntityManager = entityManager;
            m_entity = e;
            m_config = config;
            EntityManager.SetComponentData(e, new Speed(){Value =  config.MoveSpeed});
            m_logic = FiberCtrl.Pool.Run(Logic());
            return true;
        }

        /// <summary>
        /// 车辆长度信息
        /// </summary>
        public float bodyLength => m_config.BodyLength;

        /// <summary>
        /// 路径点
        /// </summary>
        public string pathTag => m_config.PathTag;

        /// <summary>
        /// 加载AI脚本
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        AssetRequest LoadAI(string name)
        {
            string path = "Assets/BuildAsset/VisualScript/Prefabs/AI/" + name + ".prefab";
            return Assets.LoadAssetAsync(path, typeof(GameObject));
        }

        /// <summary>
        /// 加载资源
        /// </summary>
        /// <returns></returns>
        IEnumerator LoadResources()
        {
            var aiReq = LoadAI(m_config.AI);
            var modelRequest = Assets.LoadAssetAsync(ASSET_PATH + m_config.Model, typeof(GameObject));
            yield return aiReq;
            yield return modelRequest;
            aiReq.Release();
            modelRequest.Release();
            aiReq.Require(gameObject);
            modelRequest.Require(gameObject);

            if (!string.IsNullOrEmpty(aiReq.error) || !string.IsNullOrEmpty(modelRequest.error))
            {
                log.Error("load car resource fail!");
                yield break;
            }
            
            m_model = GameObject.Instantiate(modelRequest.asset as GameObject, transform);
            m_ai = GameObject.Instantiate(aiReq.asset as GameObject, transform);
        }

        void SetupGameObject()
        {
            m_ai.transform.localPosition = Vector3.zero;
            m_ai.transform.localRotation = Quaternion.identity;
            
            // 设置角色位置
            if (m_config.PositionLength != 3)
                m_model.transform.localPosition = Vector3.zero;
            else
                m_model.transform.localPosition = new Vector3(m_config.Position(0), m_config.Position(1), m_config.Position(2));

            if (m_config.RotationLength != 3)
                m_model.transform.localRotation = Quaternion.identity;
            else
                m_model.transform.localRotation = Quaternion.Euler(m_config.Rotation(0), m_config.Rotation(1), m_config.Rotation(2));
            m_model.transform.localScale = new Vector3(m_config.Scale, m_config.Scale, m_config.Scale);
        }

        /// <summary>
        /// 汽车线性逻辑代码
        /// </summary>
        /// <returns></returns>
        IEnumerator Logic()
        {
            m_queue = CarQueueManager.Instance.GetOrCreate(pathTag);
            m_roads = new List<Vector3>();

            yield return LoadResources();
            if (m_model == null || m_ai == null)
                yield break;
            

            // 设置位置信息
            SetupGameObject();
            
            // 开始移动
            /*
            m_roads = MapAgent.GetRoad(m_pathTag);

            List<Vector3> roads = m_roads;
            var positionBuffer = EntityManager.GetBuffer<FPathPositions>(m_entity);
            for (int i = roads.Count - 1; i >= 0; i--)
            {
                var pos = (float3)roads[i];
                positionBuffer.Add(new FPathPositions(){Value = pos});
            }
            EntityManager.SetComponentData(m_entity, new Follow(){Value = roads.Count});

            while (IsMoveEnd == false)
                yield return null;
            
            CarModule.Instance.Close(m_entity);
            */
        }

        public bool IsMoving
        {
            get
            {
                var follow = EntityManager.GetComponentData<Follow>(m_entity);
                return follow.Value > 0;
            }
        }
        

        private void OnDestroy()
        {
            if (m_logic != null)
            {
                m_logic.Terminate();
                m_logic = null;
            }
        }

        /// <summary>
        /// 进入排队
        /// </summary>
        public void EnterQueue()
        {
            m_queue.Add(m_entity, bodyLength);
        }

        /// <summary>
        /// 离开队伍
        /// </summary>
        public void LeaveQueue()
        {
            if (m_queue.Remove(m_entity))
            {
                EventManager.Instance.AsyncTrigger((int)GameEvent.LEVELPATH_QUEUE_UPDATE, pathTag);
            }
        }

        /// <summary>
        /// 获得队伍排名
        /// </summary>
        /// <returns></returns>
        public int GetQueueOrder()
        {
            return m_queue.GetOrder(m_entity);
        }

        private void DoMove()
        {
            List<Vector3> roads = m_roads;
            var positionBuffer = EntityManager.GetBuffer<FPathPositions>(m_entity);
            positionBuffer.Clear();
            for (int i = roads.Count - 1; i >= 0; i--)
            {
                var pos = (float3)roads[i];
                positionBuffer.Add(new FPathPositions(){Value = pos});
            }
            EntityManager.SetComponentData(m_entity, new Follow(){Value = roads.Count});
        }

        /// <summary>
        /// 移动队伍
        /// </summary>
        /// <returns></returns>
        public bool Move()
        {
            // 获得角色在
            if (!m_queue.GetLinePath(m_entity, m_roads))
            {
                return false;
            }

            // 条用移动模块移动
            DoMove();
            return true;
        }

        /// <summary>
        /// 移动到结束
        /// </summary>
        /// <returns></returns>
        public bool MoveToEnd()
        {
            if (!m_queue.GetOrderToEndPath(m_roads))
                return false;

            DoMove();
            return true;
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        public void Close()
        {
            CarModule.Instance.Close(m_entity);
        }
    }
}
