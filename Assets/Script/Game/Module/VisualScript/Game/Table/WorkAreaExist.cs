using SGame;
using Unity.VisualScripting;

namespace SGame.VS
{
    /// <summary>
    /// 通过角色ID获得角色对象
    /// </summary>
	
	[UnitTitle("WorkAreaExist")] //The Custom Scripting Event node to receive the Event. Add "On" to the node title as an Event naming convention.
    [UnitCategory("Game/Table")]
    public sealed class WorkAreaExist : Unit
    {
        /// <summary>
        /// The value of the variable.
        /// </summary>
        [DoNotSerialize]
        [PortLabelHidden]
        public ValueOutput exits { get; private set; }
        
        [DoNotSerialize]
        public ValueInput area { get; private set; }

        protected override void Definition()
        {
            exits    = ValueOutput(nameof(exits), Get);//.PredictableIf(IsDefined);
            area     = ValueInput<int>("area", 0);
        }
        
        private bool Get(Flow flow)
        {
            var id = flow.GetValue<int>(this.area);
            return TableManager.Instance.HasWorkArea(id);
        }
    }
}
