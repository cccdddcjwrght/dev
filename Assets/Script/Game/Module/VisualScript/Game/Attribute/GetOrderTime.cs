using System.Collections;
using System.Collections.Generic;
using GameConfigs;
using UnityEngine;
using Unity.VisualScripting;
using SGame.UI;
using Unity.Entities;

namespace SGame.VS
{
    // 下单时间
    [UnitTitle("GetOrderTime")] 
    [UnitCategory("Game/Attribute")]
    public class GetOrderTime : BaseRoleAttribute
    {
        [DoNotSerialize]
        public ValueOutput resultTime; // 返回下单时间
        
        // 端口定义
        protected override void Definition()
        {
            base.Definition();

            
            resultTime          = ValueOutput<float>("time", GetValue);
        }

        private static float _baseOrderTime = 0;
        static float baseOrderTime
        {
            get
            {
                if (_baseOrderTime == 0)
                    _baseOrderTime = GlobalDesginConfig.GetFloat("order_time");

                return _baseOrderTime;
            }
        }

        /// <summary>
        /// 获得订单时间
        /// </summary>
        /// <param name="flow"></param>
        /// <returns></returns>
        float GetValue(Flow flow)
        {
            int roleID = GetRoleID(flow);
            double orderSpeed = AttributeSystem.Instance.GetValueByRoleID(roleID, EnumAttribute.OrderSpeed);
            return baseOrderTime / (float)orderSpeed;
        }
    }
}