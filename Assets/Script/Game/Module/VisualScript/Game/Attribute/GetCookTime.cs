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
        public ControlInput     inputTrigger;

        // 成功返回流程
        [DoNotSerialize]
        public ControlOutput    outputTrigger;
        
        // 立马返回
        [DoNotSerialize]
        public ControlOutput    outputImmediately;

        
        [DoNotSerialize]
        public ValueInput m_foodType;

        [DoNotSerialize]
        public ValueOutput resultTime;     // 返回显示时间

        private float m_foodTime = 0;
        
        // 端口定义
        protected override void Definition()
        {
            base.Definition();

            inputTrigger        = ControlInput("input", InputFunc);
            m_foodType          = ValueInput<int>("foodType");
            resultTime          = ValueOutput<float>("time", (flow) => m_foodTime);
            outputTrigger       = ControlOutput("success");
            outputImmediately   = ControlOutput("Immediately");

        }
        ControlOutput InputFunc(Flow flow)
        {
            m_foodTime = 0;
            
            // 1.4 角色制作速度：角色制作速度【J-角色属性配置表--Efficiency】，制作时间=初始加工时间/角色制作速度/加工台制作速度
            int foodType = flow.GetValue<int>(m_foodType);
            
            int machineID = TableManager.Instance.FindMachineIDFromFoodType(foodType);
            int roleID = GetRoleID(flow);
            if (machineID < 0)
            {
                log.Error("machine id not found foodType = " + foodType);
                return outputTrigger;
            }
            
            // 先判定是否是立即完成
            var immediately = AttributeSystem.Instance.GetValueByRoleID(roleID, EnumAttribute.ImmediatelyCompleteRate);
            immediately *= ConstDefine.C_PER_SCALE;
            var randomValue = RandomSystem.Instance.NextDouble(0, 1);
            if (randomValue < immediately)
                return outputImmediately;

            // 食物制作时间
            double workTime = DataCenter.MachineUtil.GetWorkTime(machineID);
            
            // 玩家制作效率
            double workSpeed = AttributeSystem.Instance.GetValueByRoleID(roleID, EnumAttribute.WorkSpeed);
            
            // 最终时间
            double finalTime = workTime / workSpeed;
            m_foodTime = (float)finalTime;
            return outputTrigger;
        }
    }
}