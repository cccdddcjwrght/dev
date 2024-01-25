using SGame;
using Unity.VisualScripting;

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
            return TableManager.Instance.Get(id);
        }
    }
}
