using System;
using log4net;
using SGame;
using Unity.VisualScripting;
using UnityEngine;
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
        private static ILog log = LogManager.GetLogger("game.table");
        /// <summary>
        /// The value of the variable.
        /// </summary>
        [DoNotSerialize]
        [PortLabelHidden]
        public ValueOutput value { get; private set; }
        
        [DoNotSerialize]
        public ValueInput tableID { get; private set; }
        
        [DoNotSerialize]
        public ValueInput characterID { get; private set; }

        protected override void Definition()
        {
            value      = ValueOutput(nameof(value), Get);//.PredictableIf(IsDefined);
            tableID     = ValueInput<int>("tableID", 0);
            characterID = ValueInput<int>("characterID");
        }
        
        private TableData Get(Flow flow)
        {
            var id = flow.GetValue<int>(this.tableID);
            var cid = flow.GetValue<int>(characterID);
            
            if (id <= 0)
            {
                log.Error("character id=" + cid);
                #if UNITY_EDITOR
                    EditorApplication.isPaused = true;
                #endif
                
                throw new Exception("table id zero or negative");
            }
            
            return TableManager.Instance.Get(id);
        }
    }
}
