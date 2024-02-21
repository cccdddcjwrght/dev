using SGame;
using Unity.VisualScripting;
using Unity.Mathematics;
using System.Collections.Generic;
using GameConfigs;
using log4net;
using Sirenix.Utilities;
using UnityEngine;

namespace SGame.VS
{
    /// <summary>
    /// 获取当前地图初始角色信息, 包含 角色类型, 角色数量, 角色集合
    /// </summary>
	[UnitTitle("GetRandomInt")] //The Custom Scripting Event node to receive the Event. Add "On" to the node title as an Event naming convention.
    [UnitCategory("Game/Random")]
    public sealed class GetRandomInt : Unit
    {
        private static ILog log = LogManager.GetLogger("game.random");
        
        [DoNotSerialize]
        public ControlInput     inputTrigger;

        // 成功返回流程
        [DoNotSerialize]
        public ControlOutput    outputSuccess;

        /// <summary>
        /// 角色ID集合
        /// </summary>
        [DoNotSerialize]
        public ValueInput m_MinValue { get; private set; }
        
        [DoNotSerialize]
        public ValueInput m_MaxValue { get; private set; }

        [DoNotSerialize]
        public ValueOutput m_OutValue;

        private int _value;
        

        protected override void Definition()
        {
            inputTrigger = ControlInput("input", (flow) =>
            {
                var min = flow.GetValue<int>(m_MinValue);
                var max = flow.GetValue<int>(m_MaxValue);
                _value = RandomSystem.Instance.NextInt(min, max + 1);
                return outputSuccess;
            });

            outputSuccess    = ControlOutput("Success");
            m_MinValue       = ValueInput<int>("min", 0);
            m_MaxValue       = ValueInput<int>("max", 1);
            m_OutValue       = ValueOutput<int>("value", (flow) => _value);
        }
    }
}
