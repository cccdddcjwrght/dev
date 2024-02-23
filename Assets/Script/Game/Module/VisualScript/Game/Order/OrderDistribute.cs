using System.Collections;
using System.Collections.Generic;
using log4net;
using UnityEngine;
using Unity.VisualScripting;
using SGame.UI;
using Unity.Entities;
using Unity.Mathematics;

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
        
        //当前人物位置
        [DoNotSerialize]
        public ValueInput rolePos;
        
        //操作台位置
        [DoNotSerialize]
        public ValueInput targetPos;


        private int   characterID;
        private int   roleSort;
        private float roleDistance;
        private int2  role_Pos;
        private int2  target_Pos;
        
        protected override void Definition()
        {
            inputTrigger = ControlInput("Input", (flow) =>
            {
                var priorityManager = PriorityManager.Instance;
                characterID = flow.GetValue<int>(id);
                roleSort = flow.GetValue<int>(sort);
                role_Pos= flow.GetValue<int2>(rolePos);
                target_Pos= flow.GetValue<int2>(targetPos);
                roleDistance = Mathf.Sqrt(((target_Pos.y - role_Pos.y) ^ 2 + (target_Pos.x - role_Pos.x) ^ 2));
                // 优先级计算
                priorityManager.SetPriority(characterID,roleSort,roleDistance);
                var priorityId=priorityManager.GetPriorityID();
                if(characterID==priorityId)
                {
                    priorityManager.RemovePriorityData(characterID);
                    return outputSuccess;
                }
                return outputFail;
            });
            
            outputSuccess  = ControlOutput("Success");
            outputFail     = ControlOutput("Fail");
            
            id           = ValueInput<int>("Id", 0);
            sort         = ValueInput<int>("Sort", 0);
            rolePos      = ValueInput<int2>("RolePos",  0);
            targetPos    = ValueInput<int2>("TargetPos",  0);

        }
    }
}