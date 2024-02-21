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
    [UnitTitle("OrderDistribute")] //The Custom Scripting Event node to receive the Event. Add "On" to the node title as an Event naming convention.
    [UnitCategory("Game/Order")]
    public class OrderDistribute : Unit
    {
        private static ILog log = LogManager.GetLogger("game.order");
        
        [DoNotSerialize]
        public ControlInput inputTrigger;

        [DoNotSerialize]
        public ControlOutput outputSuccess;
        
        [DoNotSerialize]
        public ControlOutput outputFail;
        
        //当前人物id
        [DoNotSerialize]
        public ValueInput id;
        
        //当前人物序列号
        [DoNotSerialize]
        public ValueInput sort;
        
        //当前人物工作台距离
        [DoNotSerialize]
        public ValueInput distance;


        private int   characterID;
        private int   roleSort;
        private float roleDistance;
        
        protected override void Definition()
        {
            inputTrigger = ControlInput("Input", (flow) =>
            {
                var priorityManager = PriorityManager.Instance;
                characterID = flow.GetValue<int>(id);
                roleSort = flow.GetValue<int>(sort);
                roleDistance = flow.GetValue<float>(distance);
                // 优先级计算
                priorityManager.SetPriority(characterID,roleSort,roleDistance);
                var priorityId=priorityManager.GetPriorityID();
                if (priorityId == 0)
                {
                    return outputFail;
                }
                else
                {
                    return outputSuccess;
                }
            });
            
            outputSuccess  = ControlOutput("Success");
            outputFail     = ControlOutput("Fail");
            
            id           = ValueInput<int>("Id", 0);
            sort         = ValueInput<int>("Sort", 0);
            distance     = ValueInput<float>("Distance",  0);
        }
    }
}