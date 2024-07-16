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
        public ValueInput   m_WorkerArea;

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
                var workerArea = flow.GetValue<int>(m_WorkerArea);
                var tableManager = TableManager.Instance;
                var tasks = OrderTaskManager.Instance.GetOrCreateTask(ORDER_PROGRESS.ORDED);
                var order = tasks.Pop();
                if (order == null)
                    return outputFail;

                /*
                if (!foodTypes.Contains(order.foodType))
                {
                    tasks.Add(order);
                    return outputFail;
                }
                */

                TableData t = tableManager.GetFoodTable(order.foodType);
                if (t == null || t.workAreaID != workerArea)
                {
                    // 只匹配符合区域的订单
                    tasks.Add(order);
                    return outputFail;
                }

                resultValue = order;
                resultChairData = tableManager.FindMachineChairFromFoodType(order.foodType);
                if (resultChairData.IsNull)
                {
                    tasks.Add(order);
                    return outputFail;
                }

                return outputSuccess;
            });
            
            outputSuccess  = ControlOutput("Success");
            outputFail     = ControlOutput("Fail");
            
            resultOrder    = ValueOutput<OrderData>("Order", (flow) => resultValue);
            resultChair    = ValueOutput<ChairData>("Chair", (flow) => resultChairData);
            m_WorkerArea   = ValueInput<int>("WorkerArea", 0);
        }
    }
}