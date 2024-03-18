using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using SGame.UI;
using Unity.Entities;
using log4net;
using Random = Unity.Mathematics.Random;

namespace SGame.VS
{
    // 食物生成价格
    [UnitTitle("GetFoodGold")] 
    [UnitCategory("Game/Attribute")]
    public class GetFoodGold : BaseRoleAttribute
    {
        private static ILog log = LogManager.GetLogger("game.character");
        
        [DoNotSerialize]
        public ControlInput     inputTrigger;

        // 成功返回流程
        [DoNotSerialize]
        public ControlOutput    outputTrigger;
        
        [DoNotSerialize]
        public ValueInput       m_foodType; 
        
        [DoNotSerialize]
        public ValueOutput      resultGold;
        
        [DoNotSerialize]
        public ValueOutput      resultPerfect;

        // 计算得到的金币
        private double          m_gold;
        
        // 是否是完美食物
        private bool            m_isPerfect;


        // 端口定义
        protected override void Definition()
        {
            base.Definition();

            m_foodType      = ValueInput<int>("FoodType");
            resultGold      = ValueOutput<double>("gold", (flow) => m_gold);
            resultPerfect   = ValueOutput<bool>("perfect", (flow) => m_isPerfect);
            outputTrigger = ControlOutput("output");
            inputTrigger = ControlInput("input", GetValue);
        }

        static int GetPerfectGold(int roleID)
        {
            if (ConfigSystem.Instance.TryGet(roleID, out GameConfigs.RoleDataRowData config))
            {
                return config.PerfectRatio;
            }
            
            return 1;
        }
        
        
        /// <summary>
        /// 计算食物金币
        /// </summary>
        /// <param name="flow"></param>
        /// <returns></returns>
        ControlOutput GetValue(Flow flow)
        {
            // 计算金币
            int roleID = GetRoleID(flow);
            int foodType = flow.GetValue<int>(m_foodType);
            int machineID = TableManager.Instance.FindMachineIDFromFoodType(foodType);
            if (machineID < 0)
            {
                log.Error("machine id not found foodType = " + foodType);
                return outputTrigger;
            }

            // 机器金币
            double gold = DataCenter.MachineUtil.GetWorkItemPrice(machineID);
            m_gold = gold;
            
            // 完美
            double perfectProbability = AttributeSystem.Instance.GetValueByRoleID(roleID, EnumAttribute.PerfectCompleteRate);
            double randValue = RandomSystem.Instance.NextDouble(0, 1);
            m_isPerfect = (randValue <= perfectProbability * ConstDefine.C_PER_SCALE);
            if (m_isPerfect)
            {
                // 完美金币
                int addGold = GetPerfectGold(roleID);
                m_gold *= (addGold * ConstDefine.C_PER_SCALE);
            }
            
            // 添加价格系数
            m_gold *= (AttributeSystem.Instance.GetValueByRoleID(roleID, EnumAttribute.Price) * ConstDefine.C_PER_SCALE);
            m_gold = System.Math.Floor(m_gold);
            return outputTrigger;
        }
    }
}