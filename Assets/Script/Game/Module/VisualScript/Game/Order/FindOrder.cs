using System.Collections;
using System.Collections.Generic;
using log4net;
using UnityEngine;
using Unity.VisualScripting;
using SGame.UI;
using Unity.Entities;

namespace SGame.VS
{
    [UnitTitle("FindOrder")] //The Custom Scripting Event node to receive the Event. Add "On" to the node title as an Event naming convention.
    [UnitCategory("Game/Order")]
    public class FindOrder : Unit
    {
        private static ILog log = LogManager.GetLogger("game.order");
        
        [DoNotSerialize]
        public ControlInput inputTrigger;

        [DoNotSerialize]
        public ControlOutput outputSuccess;
        
        [DoNotSerialize]
        public ControlOutput outputFail;

        
        [DoNotSerialize]
        public ValueInput _orderProgress; // 订单进度

        [DoNotSerialize]
        public ValueOutput result;
        
        private OrderData resultValue;
        
        protected override void Definition()
        {
            // 创建订单
            inputTrigger = ControlInput("Input", (flow) =>
            {
                ORDER_PROGRESS orderProgress = flow.GetValue<ORDER_PROGRESS>(this._orderProgress);
                int orderID = OrderManager.Instance.FindFirstOrder(orderProgress);
                if (orderID <= 0)
                {
                    return outputFail;
                }

                resultValue = OrderManager.Instance.Get(orderID);
                if (resultValue == null)
                {
                    log.Error("order not found id =" + orderID);
                    return outputFail;
                }
                
                return outputSuccess;
            });
            
            _orderProgress = ValueInput<ORDER_PROGRESS>("customer", ORDER_PROGRESS.WAIT);
            outputSuccess  = ControlOutput("Success");
            outputFail     = ControlOutput("Fail");
            result = ValueOutput<OrderData>("Order", (flow) => resultValue);
        }
    }
}