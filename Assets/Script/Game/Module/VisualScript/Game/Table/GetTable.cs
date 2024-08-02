using System;
using SGame;
using Unity.VisualScripting;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace SGame.VS
{
    /// <summary>
    /// 通过角色ID获得角色对象
    /// </summary>
	
	[UnitTitle("GetTable")] //The Custom Scripting Event node to receive the Event. Add "On" to the node title as an Event naming convention.
    [UnitCategory("Game/Table")]
    public sealed class GetTable : Unit
    {
        /// <summary>
        /// The value of the variable.
        /// </summary>
        [DoNotSerialize]
        [PortLabelHidden]
        public ValueOutput value { get; private set; }
        
        [DoNotSerialize]
        public ValueInput tableID { get; private set; }

        protected override void Definition()
        {
            value      = ValueOutput(nameof(value), Get);//.PredictableIf(IsDefined);
            tableID     = ValueInput<int>("tableID", 0);
        }
        
        private TableData Get(Flow flow)
        {
            var id = flow.GetValue<int>(this.tableID);
            if (id <= 0)
            {
                #if UNITY_EDITOR
                    EditorApplication.isPaused = true;
                #endif
                
                throw new Exception("table id zero or negative");
            }
            return TableManager.Instance.Get(id);
        }
    }
}
