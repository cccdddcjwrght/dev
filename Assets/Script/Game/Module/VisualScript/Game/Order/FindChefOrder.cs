using System.Collections;
using System.Collections.Generic;
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
                var foodTypes = tableManager.GetOpenFoodTypes(); // 获得所有开启的食物
                var foodAreas = tableManager.GetOpenArea();
                for (int i = 0; i  < foodTypes.Count;i++)
                {
                    var foodType = foodTypes[i];
                    var area = foodAreas[i];

                    if (area == workerArea)
                    {
                        // 确定有空位
                        resultChairData = tableManager.FindMachineChairFromFoodType(foodType);
                        if (!resultChairData.IsNull)
                        {
                            // 订单中查询
                            resultValue = OrderManager.Instance.FindChefOrder(foodType);
                            if (resultValue != null)
                                return outputSuccess;
                        }
                    }
                }
                
                return outputFail;
            });
            
            outputSuccess  = ControlOutput("Success");
            outputFail     = ControlOutput("Fail");
            
            resultOrder    = ValueOutput<OrderData>("Order", (flow) => resultValue);
            resultChair    = ValueOutput<ChairData>("Chair", (flow) => resultChairData);
            m_WorkerArea   = ValueInput<int>("WorkerArea", 0);
        }
    }
}