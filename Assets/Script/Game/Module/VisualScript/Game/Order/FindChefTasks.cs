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
    [UnitTitle("FindChefTasks")] //The Custom Scripting Event node to receive the Event. Add "On" to the node title as an Event naming convention.
    [UnitCategory("Game/Order")]
    public class FindChefTasks : Unit
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

        // 返回列表
        [DoNotSerialize]
        private AotList m_resultObject = new AotList();
        
        // 对应的座位
        // 返回机器台
        [DoNotSerialize]
        public ValueOutput resultValue;

        private OrderData   m_resultValue;
        //private ChairData   resultChairData;
        
        protected override void Definition()
        {
            m_resultObject.Clear();
            
            // 创建订单
            inputTrigger = ControlInput("Input", (flow) =>
            {
                m_resultObject.Clear();
                var workerArea = flow.GetValue<int>(m_WorkerArea);
                var tableManager = TableManager.Instance;
                var tasks = OrderTaskManager.Instance.GetOrCreateTask(ORDER_PROGRESS.ORDED);

                for (int i = 0; i < tasks.Count; i++)
                {
                    var task = tasks.Get(i);
                    TableData t = tableManager.GetFoodTable(task.m_order.foodType);
                    if (t == null || t.workAreaID != workerArea)
                    {
                        // 只匹配符合区域的订单
                        continue;
                    }
                    
                    // 已经获取过了
                    if (task.isReadly) //.m_order.cookerID == -1)
                    {
                        m_resultObject.Add(task);
                        continue;
                    }
                    
                    // 找到可用的桌子
                    var workChair = tableManager.FindMachineChairFromFoodType(task.m_order.foodType);
                    if (workChair.IsNull)
                        continue;

                    // 锁定座位
                    tableManager.SitChair(workChair, -1);
                    
                    // 保存座位
                    task.m_order.CookerTake(-1, workChair);

                    // 添加
                    m_resultObject.Add(task);
                }

                if (m_resultObject.Count == 0)
                    return outputFail;

                return outputSuccess;
            });
            
            outputSuccess  = ControlOutput("Success");
            outputFail     = ControlOutput("Fail");
            
            resultValue    = ValueOutput<AotList>("Tasks", (flow) => m_resultObject);
            m_WorkerArea   = ValueInput<int>("WorkerArea", 0);
        }
    }
}