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
    /// 获取公交座位信息
    /// </summary>
	
	[UnitTitle("GetBusSitInfo")] 
    [UnitCategory("Game/Car")]
    public sealed class GetBusSitInfo : Unit
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
        /// 汽车对象
        /// </summary>
        [DoNotSerialize]
        public ValueInput busEntity { get; private set; }
        
        /// <summary>
        /// 创建的角色Entity
        /// </summary>
        public ValueInput chairIndex { get; private set; }
        
        /// <summary>
        /// 位置信息
        /// </summary>
        public ValueOutput position3d { get; private set; }
        
        /// <summary>
        /// 位置旋转信息
        /// </summary>
        public ValueOutput angle { get; private set; }
        
        public ValueOutput customerEntity { get; private set; }
        
        public ValueOutput IsNoLeave { get; private set; }


        private Vector3 m_position;

        private float m_angle;

        private Entity m_customerEntity = Entity.Null;

        private bool m_IsNoLeave = false;

        protected override void Definition()
        {
            inputTrigger = ControlInput("Input", (flow) =>
            {
                var bus = flow.GetValue<Entity>(busEntity);
                var index = flow.GetValue<int>(chairIndex);
                EntityManager entityManger = World.DefaultGameObjectInjectionWorld.EntityManager;

                if (!entityManger.HasComponent<CarMono>(bus))
                {
                    log.Error("CarMono component not found=" + bus);
                    return failTrigger;
                }

                var car = entityManger.GetComponentObject<CarMono>(bus);
                var seats = car.seats;

                m_position = seats.GetSeatPosition(index);
                var rot = seats.GetSeatRotation(index);
                m_angle = Utils.GetRotationAngle(rot);
                m_customerEntity = seats.GetSeat(index).customer;
                m_IsNoLeave = seats.GetSeat(index).state == CarCustomer.SeatState.NOLEAVE;
                return successTrigger;
            });

            busEntity           = ValueInput<Entity>("bus");
            chairIndex          = ValueInput<int>("chairIndex", 0);
            position3d            = ValueOutput<Vector3>("position", (flow) => m_position);
            angle               = ValueOutput<float>("angle", (flow) => m_angle);
            customerEntity      = ValueOutput<Entity>("customer", (flow) => m_customerEntity);
            IsNoLeave = ValueOutput<bool>("isNotLeave", (flow) => m_IsNoLeave);
            successTrigger    = ControlOutput("Success");
            failTrigger       = ControlOutput("Fail");
        }
    }
}
