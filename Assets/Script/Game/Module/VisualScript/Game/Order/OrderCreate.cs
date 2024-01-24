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
        public ValueOutput result;
        
        private OrderData resultValue;
        
        protected override void Definition()
        {
            // 创建订单
            inputTrigger = ControlInput("Input", (flow) =>
            {
                int customer = flow.GetValue<int>(this.customerID);
                resultValue = OrderManager.Instance.Create(customer);
                return outputTrigger;
            });
            
            customerID = ValueInput<int>("customer", 0);
            outputTrigger = ControlOutput("Output");
            result = ValueOutput<OrderData>("Order", (flow) => resultValue);
        }
    }
}