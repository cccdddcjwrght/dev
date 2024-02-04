using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using SGame.UI;
using Unity.Entities;
using log4net;

namespace SGame.VS
{
    // 食物生成价格
    [UnitTitle("GetFoodGold")] 
    [UnitCategory("Game/Attribute")]
    public class GetFoodGold : Unit
    {
        private static ILog log = LogManager.GetLogger("game.character");

        [DoNotSerialize]
        public ValueInput m_target; // 目标类型
        
        [DoNotSerialize]
        public ValueInput m_foodType; 
        
        [DoNotSerialize]
        public ValueOutput resultGold;

        private INPUT_TYPE _inputType;
        
        [DoNotSerialize]
        [Inspectable, UnitHeaderInspectable("Type")]
        public INPUT_TYPE inputType
        {
            get => _inputType;
            set => _inputType = value;
        }
        
        // 端口定义
        protected override void Definition()
        {
            if (inputType == INPUT_TYPE.ID)
            {
                m_target = ValueInput<int>("CharacterID");
            }
            else
            {
                m_target = ValueInput<Character>("Character");
            }

            m_foodType = ValueInput<int>("FoodType");
            resultGold  = ValueOutput<double>("gold", GetValue);
        }

        /// <summary>
        /// 计算食物金币
        /// </summary>
        /// <param name="flow"></param>
        /// <returns></returns>
        double GetValue(Flow flow)
        {
            int foodType = flow.GetValue<int>(m_foodType);
            int machineID = TableManager.Instance.FindMachineIDFromFoodType(foodType);
            if (machineID < 0)
            {
                log.Error("machine id not found foodType = " + foodType);
                return 0f;
            }

            double gold = DataCenter.MachineUtil.GetWorkItemPrice(machineID);
            return gold;
        }
    }
}