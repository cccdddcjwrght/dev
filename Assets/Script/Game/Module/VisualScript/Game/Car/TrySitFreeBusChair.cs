using log4net;
using SGame;
using Unity.VisualScripting;
using Unity.Mathematics;
using UnityEngine;
using System.Collections.Generic;
using SGame.Firend;
using Unity.Entities;

namespace SGame.VS
{
    /// <summary>
    /// 尝试坐下 空闲汽车空闲车位
    /// </summary>
	
	[UnitTitle("TrySitFreeBusChair")] 
    [UnitCategory("Game/Car")]
    public sealed class TrySitFreeBusChair : Unit
    {
        private static ILog log = LogManager.GetLogger("game.car");
        
        [DoNotSerialize]
        public ControlInput     inputTrigger;

        // 成功返回流程
        [DoNotSerialize]
        public ControlOutput    successTrigger;
        
        // 生工返回流程
        [DoNotSerialize]
        public ControlOutput    failTrigger;
        
        
        /// <summary>
        /// 顾客对象
        /// </summary>
        [DoNotSerialize]
        public ValueInput customerEntity { get; private set; }
        
        /// <summary>
        /// 线路
        /// </summary>
        [DoNotSerialize]
        public ValueInput pathTag { get; private set; }

        /// <summary>
        /// 汽车对象
        /// </summary>
        [DoNotSerialize]
        public ValueOutput busEntity { get; private set; }
        
        /// <summary>
        /// 创建的角色Entity
        /// </summary>
        public ValueOutput chairIndex { get; private set; }
        private int  m_chairIndex;

        private Entity m_busEntity;

        protected override void Definition()
        {
            inputTrigger = ControlInput("Input", (flow) =>
            {
                var tag = flow.GetValue<string>(pathTag);
                var customer = flow.GetValue<Entity>(customerEntity);
                
                CarQueue carQueue = CarQueueManager.Instance.GetOrCreate(tag);
                if (carQueue == null)
                {
                    log.Error("pathTag not found=" + tag);
                    return failTrigger;
                }

                var bus = carQueue.GetFirst();
                var EntityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
                if (!EntityManager.HasComponent<CarMono>(bus))
                {
                    log.Error("no car mono script=" + tag);
                    return failTrigger;
                }
                var busMono = EntityManager.GetComponentObject<CarMono>(bus);
                var seats = busMono.seats;
                int emptySeatIndex = seats.GetEmptySeat();
                if (emptySeatIndex < 0)
                {
                    return failTrigger;
                }
                
                // 坐下位置
                if (!seats.SitEmptySeat(customer, emptySeatIndex))
                {
                    log.Error("sit emtpty fail");
                    return failTrigger;
                }
                
                // 返回位置信息, 用于角色移动过去
                m_chairIndex = emptySeatIndex;
                m_busEntity = busMono.entity;
                return successTrigger;
            });

            pathTag           = ValueInput<string>("pathTag");
            customerEntity    = ValueInput<Entity>("customer");
            busEntity         = ValueOutput<Entity>("bus", (flow)=> m_busEntity);
            chairIndex        = ValueOutput<int>("chairIndex", (flow)=> m_chairIndex);
            successTrigger    = ControlOutput("Success");
            failTrigger       = ControlOutput("Fail");
        }
    }
}
