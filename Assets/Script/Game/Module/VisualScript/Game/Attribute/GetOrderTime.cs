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

        /// <summary>
        /// 获得订单时间
        /// </summary>
        /// <param name="flow"></param>
        /// <returns></returns>
        float GetValue(Flow flow)
        {
            int roleID = GetRoleID(flow);
            return TableUtils.GetOrderTime(roleID);
        }
    }
}