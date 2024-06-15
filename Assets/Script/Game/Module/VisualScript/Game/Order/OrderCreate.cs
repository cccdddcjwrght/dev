using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using SGame.UI;
using Unity.Entities;

namespace SGame.VS
{
    [UnitTitle("OrderCreate")] //The Custom Scripting Event node to receive the Event. Add "On" to the node title as an Event naming convention.
    [UnitCategory("Game/Order")]
    public class OrderCreate : Unit
    {
        [DoNotSerialize]
        public ControlInput inputTrigger;

        [DoNotSerialize]
        public ControlOutput outputTrigger;
        
        [DoNotSerialize]
        public ValueInput customerID; // 角色ID
        
        [DoNotSerialize]
        public ValueInput foodType; // 食物类型

        [DoNotSerialize]
        public ValueOutput result;

        [DoNotSerialize]
        public ValueOutput outFoodType;
        
        private OrderData resultValue;

        private int resultFoodType;
        
        protected override void Definition()
        {
            // 创建订单
            inputTrigger = ControlInput("Input", (flow) =>
            {
                ChairData customer = flow.GetValue<ChairData>(this.customerID);
                int food = flow.GetValue<int>(this.foodType);
                resultFoodType = food;
                resultValue = OrderManager.Instance.Create(customer, food);
                return outputTrigger;
            });
            
            customerID = ValueInput<ChairData>("customer");
            foodType = ValueInput<int>("foodType", 0);
            outputTrigger = ControlOutput("Output");
            result = ValueOutput<OrderData>("Order", (flow) => resultValue);
            outFoodType = ValueOutput<int>("foodType", (flow) => resultFoodType);
        }
    }
}