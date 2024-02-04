using System.Collections;
using System.Collections.Generic;
using GameConfigs;
using log4net;
using UnityEngine;
using Unity.VisualScripting;
using SGame.UI;
using Unity.Entities;

namespace SGame.VS
{
    // 角色煮饭时间
    [UnitTitle("GetCookTime")] 
    [UnitCategory("Game/Attribute")]
    public class GetCookTime : BaseRoleAttribute
    {
        private static ILog log = LogManager.GetLogger("game.character");
        
        [DoNotSerialize]
        public ValueInput m_foodType;
        
        [DoNotSerialize]
        public ValueOutput resultTime;     // 返回显示时间

        static private float _baseFoodTime = 0.0f;

        // 端口定义
        protected override void Definition()
        {
            base.Definition();

            m_foodType = ValueInput<int>("foodType");
            resultTime   = ValueOutput<float>("time", GetValue);
        }

        float GetValue(Flow flow)
        {
            // 1.4 角色制作速度：角色制作速度【J-角色属性配置表--Efficiency】，制作时间=初始加工时间/角色制作速度/加工台制作速度
            int foodType = flow.GetValue<int>(m_foodType);
            int machineID = TableManager.Instance.FindMachineIDFromFoodType(foodType);
            if (machineID < 0)
            {
                log.Error("machine id not found foodType = " + foodType);
                return 0f;
            }

            double workTime = DataCenter.MachineUtil.GetWorkTime(machineID);
            return (float)(workTime);
        }
    }
}