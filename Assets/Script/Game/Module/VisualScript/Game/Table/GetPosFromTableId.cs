using SGame;
using Unity.VisualScripting;
using GameConfigs;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

namespace SGame.VS
{
    /// <summary>
    /// 通过角色类型, 随机获得角色ID
    /// </summary>
	
	[UnitTitle("GetPosFromTableId")] //The Custom Scripting Event node to receive the Event. Add "On" to the node title as an Event naming convention.
    [UnitCategory("Game/Table")]
    public sealed class GetPosFromTableId : Unit
    {
        [DoNotSerialize]
        public ControlInput     inputTrigger;

        // 成功返回流程
        [DoNotSerialize]
        public ControlOutput    successTrigger;
        
        [DoNotSerialize]
        public ControlOutput    failTrigger;
        
        /// <summary>
        /// The value of the variable.
        /// </summary>
        [DoNotSerialize]
        [PortLabelHidden]
        public ValueOutput value { get; private set; }

        /// <summary>
        /// The value to return if the variable is not defined.
        /// </summary>
        [DoNotSerialize]
        public ValueInput tableID { get; private set; }

        private int2 m_resultValue;

        protected override void Definition()
        {
            inputTrigger = ControlInput("Input", (flow) =>
            {
                var tableID = flow.GetValue<int>(this.tableID);
                m_resultValue = int2.zero;
                return successTrigger;
            });
            value           = ValueOutput(nameof(value), Get);
            tableID         = ValueInput<int>("tableID", 0);
            successTrigger  = ControlOutput("success");
            failTrigger     = ControlOutput("fail");
        }
        
        private int2 Get(Flow flow)
        {
            var characterType = flow.GetValue<int>(this.tableID);
            Debug.LogError("not found");
            return int2.zero;
        }
    }
}
