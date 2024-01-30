using SGame;
using Unity.VisualScripting;
using GameConfigs;
using System.Collections.Generic;
using log4net;

namespace SGame.VS
{
    /// <summary>
    /// 通过角色类型, 随机获得角色ID
    /// </summary>
	
	[UnitTitle("GetRoleFromType")] //The Custom Scripting Event node to receive the Event. Add "On" to the node title as an Event naming convention.
    [UnitCategory("Game/Character")]
    public sealed class GetRoleFromType : Unit
    {
        private static ILog log = LogManager.GetLogger("game.character");
        
        [DoNotSerialize]
        public ControlInput     inputTrigger;

        // 成功返回流程
        [DoNotSerialize]
        public ControlOutput    outputTrigger;
        
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
        public ValueInput characterType { get; private set; }

        private int m_valueResult;

        protected override void Definition()
        {
            inputTrigger = ControlInput("Input", (flow) =>
            {
                var characterType = flow.GetValue<int>(this.characterType);

                List<RoleDataRowData> values = new List<RoleDataRowData>();
                if (ConfigSystem.Instance.TryGets((v) => v.Type == characterType, out values))
                {
                    int index = RandomSystem.Instance.NextInt(0, values.Count);
                    m_valueResult = values[index].Id;
                    return outputTrigger;
                }

                return failTrigger;
            });
            
            outputTrigger = ControlOutput("Sucess");
            failTrigger   = ControlOutput("Fail");
            value         = ValueOutput(nameof(value), (flow) => m_valueResult);
            characterType = ValueInput<int>("characterType", 0);
        }
        
        private int Get(Flow flow)
        {
            var characterType = flow.GetValue<int>(this.characterType);

            List<RoleDataRowData> values = new List<RoleDataRowData>();
            if (ConfigSystem.Instance.TryGets((v) => v.Type == characterType, out values))
            {
                int index = RandomSystem.Instance.NextInt(0, values.Count);
                return values[index].Id;
            }

            return 0;
        }
    }
}
