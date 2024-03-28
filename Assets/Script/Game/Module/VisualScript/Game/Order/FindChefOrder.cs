using System.Collections;
using System.Collections.Generic;
using System.Linq;
using log4net;
using UnityEngine;
using Unity.VisualScripting;
using SGame.UI;
using Unity.Entities;

namespace SGame.VS
{
    // 厨师查找可用订单
    [UnitTitle("FindChefOrder")] //The Custom Scripting Event node to receive the Event. Add "On" to the node title as an Event naming convention.
    [UnitCategory("Game/Order")]
    public class FindChefOrder : Unit
    {
        private static ILog log = LogManager.GetLogger("game.order");
        
        [DoNotSerialize]
        public ControlInput inputTrigger;

        [DoNotSerialize]
        public ControlOutput outputSuccess;
        
        [DoNotSerialize]
        public ControlOutput outputFail;

        // 返回订单
        [DoNotSerialize]
        public ValueOutput resultOrder;
        
        // 对应的座位
        // 返回机器台
        [DoNotSerialize]
        public ValueOutput resultChair;

        private OrderData   resultValue;
        private ChairData   resultChairData;
        
        protected override void Definition()
        {
            // 创建订单
            inputTrigger = ControlInput("Input", (flow) =>
            {
                var tableManager = TableManager.Instance;
                var foodTypes = tableManager.GetOpenFoodTypes(); // 获得所有开启的食物
                var orders = OrderManager.Instance.FindOrders(ORDER_PROGRESS.ORDED);
                foreach (var id in orders)
                {
                    var item = OrderManager.Instance.Get(id);
                    if (foodTypes.Contains(item.foodType))
                    {
                        resultChairData = tableManager.FindMachineChairFromFoodType(item.foodType);
                        if (!resultChairData.IsNull)
                        {
                            resultValue = item;
                            return outputSuccess;
                        }
                    }
                }

                return outputFail;
            });
            
            outputSuccess  = ControlOutput("Success");
            outputFail     = ControlOutput("Fail");
            
            resultOrder         = ValueOutput<OrderData>("Order", (flow) => resultValue);
            resultChair          = ValueOutput<ChairData>("Chair", (flow) => resultChairData);
        }
    }
}