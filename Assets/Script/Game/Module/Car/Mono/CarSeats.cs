using System;
using System.Collections.Generic;
using GameConfigs;
using log4net;
using Unity.Entities;
using UnityEngine;
using Fibers;
using System.Collections;
using Unity.Mathematics;
using Unity.Transforms;


namespace SGame
{
    /// <summary>
    /// 汽车座位
    /// </summary>
    public class CarSeats
    {
        private static ILog log = LogManager.GetLogger("xl.game.car");
        private List<CarCustomer>           m_customers; // 座位上的玩家
        public int customerNum => m_customers.Count;
        
        private List<Transform>             m_hudAttachement;  // HUD 挂点
        private List<Transform>             m_seatAttachement; // 座位挂点
        private EntityManager               EntityManager;
        private Transform                   m_transform;
        private Entity                      m_entity;

        public CarSeats(Entity e, Transform transform, List<Transform> hud, List<Transform> seat)
        {
            m_hudAttachement = hud;
            m_seatAttachement = seat;
            m_transform = transform;
            m_entity = e;
            EntityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        }

        /// <summary>
        /// 创建车内顾客
        /// </summary>
        public void SetupCustomer(int chairNum)
        {
            m_customers = new List<CarCustomer>();
            int currentLevelID = DataCenter.Instance.GetUserData().scene;
            if (!ConfigSystem.Instance.TryGet(currentLevelID, out LevelRowData config))
            {
                log.Error("not found scene id=" + currentLevelID);
                return;
            }

            List<int> roles = new List<int>();
            List<int> widgets = new List<int>();
            for (int i = 0; i < config.CustomerIdLength; i++)
                roles.Add(config.CustomerId(i));
            for (int i = 0; i < config.CustomerWeightLength; i++)
                widgets.Add(config.CustomerWeight(i));
            if (chairNum == 0)
            {
                log.Error("chair num can't be zero");
                return;
            }
            for (int i = 0; i < chairNum; i++)
            {
                int roleID = RandomSystem.Instance.GetRandomID(roles, widgets);
                if (roleID == 0)
                {
                    log.Error("role id = 0");
                }
                m_customers.Add(new CarCustomer(){RoleID = roleID, ItemID = 0, ItemNum = 0, hud = Entity.Null, customer = Entity.Null});
            }
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
            if (itemNum <= 0 || itemID == 0)
            {
                log.Error("param not valid itemid=" + itemID + " itemNum=" + itemNum);
                return;
            }

            if (customerIndex < 0 || customerIndex >= m_customers.Count)
            {
                log.Error("customerIndex not valid customerIndex=" + customerIndex);
                return;
            }
            
            // 设置角色订单信息
            var customer        = m_customers[customerIndex];
            customer.ItemID     = itemID;
            customer.ItemNum    = itemNum;

            // 创建订单
            for (int i = 0; i < itemNum; i++)
                OrderManager.Instance.Create(chair, itemID);
            
            // 创建并关连HUD
            customer.hud = UIUtils.ShowOrderTips(itemID, itemNum, m_hudAttachement[customerIndex]);
        }
        
        /// <summary>
        /// 结束点单
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public int FinishOrder(int orderID)
        {
            var order = OrderManager.Instance.Get(orderID);
            bool findIt = false;
            foreach (var customer in m_customers)
            {
                if (customer.ItemNum > 0 && customer.ItemID == order.foodType)
                {
                    // 找到相关点单数据, 更新面版
                    customer.ItemNum--;
                    
                    // 更新小费数量
                    if (customer.ItemNum > 0)
                    {
                        UIUtils.TriggerUIEvent(customer.hud, "OrderNumUpdate", customer.ItemNum);
                    }
                    else
                    {
                        UIUtils.CloseUI(customer.hud);
                        customer.hud = Entity.Null;
                    }
                    findIt = true;
                    break;
                }
            }

            if (!findIt)
            {
                log.Error("order not match=" + orderID);
            }
            
            return GetOrderNum();
        }
        
        /// <summary>
        /// 创建船上顾客 
        /// </summary>
        /// <returns></returns>
        public IEnumerator CreateCustomer(Entity entity, Transform transform, int customerAI)
        {
            List<CharacterSpawnResult> spawnResult = new List<CharacterSpawnResult>();
            yield return new Fiber.OnTerminate(() =>
            {
                // 在加载中被销毁
                for (int i = 0; i < spawnResult.Count; i++)
                    spawnResult[i].Close();
            });
            
            int roleAI = customerAI;
            for (int i = 0; i < m_customers.Count; i++)
            {
                var customer = m_customers[i];
                spawnResult.Add(CharacterModule.Instance.CreateCarCustomer(customer.RoleID, roleAI, m_seatAttachement[i].position));
            }

            // 等待角色全部创建完毕
            for (int i = 0; i < spawnResult.Count; i++)
            {
                while (spawnResult[i].IsReadly() == false)
                    yield return null;
            }

            // 保存返回值
            for (int i = 0; i < m_customers.Count; i++)
            {
                m_customers[i].customer = spawnResult[i].entity;
                
                // 设置角色位置
                UpdateChairCustomer(i);
            }
        }
        
        public int GetOrderNum()
        {
            int orderNum = 0;
            for (int i = 0; i < m_customers.Count; i++)
                orderNum += m_customers[i].ItemNum;

            return orderNum;
        }
        
        public void ClearHud()
        {
            foreach (var customer in m_customers)
            {
                if (customer.hud != Entity.Null)
                {
                    UIUtils.CloseUI(customer.hud);
                    customer.hud = Entity.Null;
                }
            }
        }
        
        public void ClearCahracter()
        {
            foreach (var customer in m_customers)
            {
                if (customer.customer != Entity.Null)
                {
                    EntityManager.AddComponent<DespawningEntity>(customer.customer);
                    customer.customer = Entity.Null;
                }
            }
        }
        
        /// <summary>
        /// 获得空位
        /// </summary>
        /// <returns></returns>
        public int GetEmptySeat()
        {
            for (int i = 0; i < m_customers.Count; i++)
            {
                if (m_customers[i].IsEmptySeat)
                {
                    return i;
                }
            }

            return -1;
        }
        
        public CarCustomer GetSeat(int seatIndex)
        {
            return m_customers[seatIndex];
        }
        
        /// <summary>
        /// 获得位置坐标
        /// </summary>
        /// <param name="seatIndex"></param>
        /// <returns></returns>
        public Vector3 GetSeatPosition(int seatIndex)
        {
            return m_seatAttachement[seatIndex].position;
        }

        /// <summary>
        /// 获得旋转位置
        /// </summary>
        /// <param name="seatIndex"></param>
        /// <returns></returns>
        public Quaternion GetSeatRotation(int seatIndex)
        {
            return m_seatAttachement[seatIndex].rotation;
        }

        public float GetSeatAngle(int seatIndex)
        {
            return Utils.GetRotationAngle(GetSeatRotation(seatIndex));
        }

        // 坐上空椅子
        public bool SitEmptySeat(Entity customer, int seatIndex)
        {
            CarCustomer chair = GetSeat(seatIndex);
            return chair.ReturnBegin(customer);
        }

        /// <summary>
        /// 完成座位
        /// </summary>
        /// <param name="seatIndex"></param>
        public void UpdateChairCustomer(int seatIndex)
        {
            CarCustomer chair = GetSeat(seatIndex);
            if (chair != null)
            {
                int i = seatIndex;
                var customer = m_customers[i].customer;
                Vector3 childPos = m_seatAttachement[i].position - m_transform.position;
                Quaternion localRot = m_seatAttachement[i].rotation * Quaternion.Inverse(m_transform.rotation);

                // 设置位置
                EntityManager.SetComponentData(customer, new Translation(){Value = childPos});
                
                // 设置旋转
                if (!EntityManager.HasComponent<Rotation>(customer))
                    EntityManager.AddComponentData(customer, new Rotation(){Value = localRot});
                else
                    EntityManager.SetComponentData(customer, new Rotation(){Value = localRot});
                
                // 设置父节点
                if (!EntityManager.HasComponent<Parent>(customer))
                    EntityManager.AddComponentData(customer, new Parent(){Value = m_entity});
                else
                    EntityManager.SetComponentData(customer, new Parent(){Value = m_entity});
                
                // 设置转换节点
                if (!EntityManager.HasComponent<LocalToParent>(customer))
                    EntityManager.AddComponent<LocalToParent>(customer);
            }
        }

        /// <summary>
        /// 离开椅子
        /// </summary>
        /// <param name="seatIndex"></param>
        /// <returns></returns>
        public bool LeaveChair(int seatIndex)
        {
            CarCustomer chair = GetSeat(seatIndex);
            if (chair.state != CarCustomer.SeatState.NOLEAVE)
            {
                // 椅子状态不对
                log.Error("state not match =" + chair.state);
                return false;
            }

            if (chair.customer == Entity.Null)
            {
                // 没有顾客在位置上
                log.Error("no customer in chair");
                return false;
            }

            EntityManager.RemoveComponent<Parent>(chair.customer);
            EntityManager.RemoveComponent<LocalToParent>(chair.customer);
            EntityManager.SetComponentData<Rotation>(chair.customer, new Rotation(){Value = m_transform.rotation});
            EntityManager.SetComponentData<Translation>(chair.customer, new Translation() { Value = m_transform.position });
            
            //chair.customer = Entity.Null;
            chair.Leave();
            //chair.state = CarCustomer.SeatState.LEAVE;
            return true;
        }

        /// <summary>
        /// 角色回归
        /// </summary>
        /// <param name="seatIndex"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public bool ReturnChairEnd(int seatIndex, Entity customer)
        {
            if (customer == Entity.Null)
                throw new Exception("entity is null");
            
            var seat = GetSeat(seatIndex);
            bool ret = seat.ReturnEnd(customer);
            if (ret)
                UpdateChairCustomer(seatIndex);
            return ret;
        }

        /// <summary>
        /// 清空所有信息
        /// </summary>
        public void Clear()
        {
            ClearHud();
            ClearCahracter();
        }

        /// <summary>
        /// 是否所有位置都已经准备好了, 这样船就能开走了
        /// </summary>
        public bool IsReadyToLeave
        {
            get
            {
                foreach (var customer in m_customers)
                {
                    if (customer.state != CarCustomer.SeatState.RETURN)
                        return false;
                }

                return true;
            }
        }

        public int GetCustomerNum(CarCustomer.SeatState state)
        {
            int i = 0;
            foreach (var customer in m_customers)
            {
                if (customer.state == state)
                    i++;
            }

            return i;
        }
        
    }
}
