using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Fibers;
using GameConfigs;
using GameTools;
using UnityEngine;
using Unity.Entities;
using libx;
using log4net;
using Unity.Mathematics;
using Unity.Transforms;

namespace SGame
{
    public class CarMono : MonoBehaviour
    {
        private const int MAX_CUSTOMER = 4; // 最大乘客数量
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
        private int                         m_nextIndex; // 当前位置
        //private List<CarCustomer>           m_customers; // 座位上的玩家
        private CarSeats                    m_seats;          // 汽车上的座位
        private List<Transform>             m_hudAttachement; // HUD 挂点
        private List<Transform>             m_seatAttachement; // 座位挂点

        public GameObject                   script => m_ai;

        public Entity                       entity => m_entity;
        
        //public Entity                       hud;

        public int                          customerNum => m_seats.customerNum;

        private bool                        m_isInit = false;

        public bool                         isInit => m_isInit;

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
            m_nextIndex = 0;
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
        /// 队列ID
        /// </summary>
        public int queueID => m_queue.queueID;

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

        /// <summary>
        /// 设置AI 与 CAR
        /// </summary>
        void SetupGameObject()
        {
            m_model.name = "Model";
            m_ai.name = "AI";

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
            
            //CharacterModule.Instance.Create()
        }

        void SetupAttachement()
        {
            // 设置挂点
            m_hudAttachement = new List<Transform>();
            for (int i = 0; i < MAX_CUSTOMER; i++)
            {
                string hudName = "order" + (i + 1);
                Transform attachement = m_model.transform.Find(hudName);
                if (attachement == null)
                    break;
                
                m_hudAttachement.Add(attachement);
            }
            
            // 设置座位挂点
            m_seatAttachement = new List<Transform>();
            for (int i = 0; i < MAX_CUSTOMER; i++)
            {
                string hudName = "seat_role_" + (i + 1);
                Transform attachement = m_model.transform.Find(hudName);
                if (attachement == null)
                    break;
                
                m_seatAttachement.Add(attachement);
            }
        }

        /// <summary>
        /// 创建车内顾客
        /// </summary>
        void SetupCustomer()
        {
            m_seats = new CarSeats(m_entity, transform, m_hudAttachement, m_seatAttachement);
            m_seats.SetupCustomer(m_config.ChairNum);
        }

        /// <summary>
        /// 创建顾客的订单
        /// </summary>
        /// <param name="customerIndex"></param>
        ///  <param name="chair">点单时的座位信息</param>
        /// <param name="itemID">点单的道具ID</param>
        /// <param name="itemNum">点单的数量</param>
        public void CreateCustomerOrder(int customerIndex, ChairData chair, int itemID, int itemNum)
        {
            m_seats.CreateCustomerOrder(customerIndex, chair, itemID, itemNum);
        }

        /// <summary>
        /// 结束点单
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public int FinishOrder(int orderID) => m_seats.FinishOrder(orderID);

        public int GetOrderNum() => m_seats.GetOrderNum();

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
            
            // 设置挂点
            SetupAttachement();

            // 设置顾客
            SetupCustomer();

            // 创建顾客
            if (m_config.ShowCustomer != 0)
                yield return m_seats.CreateCustomer(m_entity, transform, m_config.CustomerAI);

            m_isInit = true;
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

            m_seats.Clear();
        }

        void ClearCahracter() => m_seats.ClearCahracter();

        public void ClearHud() => m_seats.ClearHud();

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
                EventManager.Instance.AsyncTrigger((int)GameEvent.LEVELPATH_QUEUE_UPDATE, pathTag, m_queue.queueID);
            }
        }

        /// <summary>
        /// 用于匹配是否是同一个队列
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="queueID"></param>
        /// <returns></returns>
        public bool IsMatchPath(string tag, int queueID)
        {
            if (queueID == 0)
                return tag == pathTag;

            return queueID == this.queueID;
        }

        /// <summary>
        /// 获得队伍排名
        /// </summary>
        /// <returns></returns>
        public int GetQueueOrder()
        {
            return m_queue.GetOrder(m_entity);
        }

        /// <summary>
        /// 汽车移动
        /// </summary>
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
            int curIndex = m_queue.GetLinePath(m_entity, m_nextIndex, m_roads);
            if (curIndex < 0)
            {
                return false;
            }

            // 条用移动模块移动
            m_nextIndex = curIndex + 1;
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

        public Transform GetHudLink(int i)
        {
            return transform;
        }

        public CarSeats seats => m_seats;

        /// <summary>
        /// 公交站点
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Vector2Int GetBusStop(int index)
        {
            return m_queue.busStopses[index];
        }

        public int BusStopNum => m_queue.busStopses.Count;
        
        // 判断顾客是否都回来了
        public bool IsReadyToLeave => m_seats.IsReadyToLeave;

        public bool LeaveChair(int chairIndex) => m_seats.LeaveChair(chairIndex);

        public bool ReturnEnd(int chairIndex, Entity customer) => m_seats.ReturnChairEnd(chairIndex, customer);

        public void UpdateChairCustomer(int chairIndex) => m_seats.UpdateChairCustomer(chairIndex);

        /// <summary>
        /// 是否需要下车
        /// </summary>
        /// <returns></returns>
        public bool NeedTakeOff()
        {
            // 队伍排名
            int order = GetQueueOrder();
            if (order >= m_queue.busStopses.Count)
            {
                // 不在公交队列中
                return false;
            }
            
            // 必须座位上有人才能下车
            return m_seats.GetCustomerNum(CarCustomer.SeatState.NOLEAVE) > 0;
        }
    }
}
