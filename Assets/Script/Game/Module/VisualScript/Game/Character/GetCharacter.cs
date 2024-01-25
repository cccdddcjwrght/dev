using SGame;
using Unity.VisualScripting;

namespace SGame.VS
{
    /// <summary>
    /// 通过角色ID获得角色对象
    /// </summary>
	
	[UnitTitle("GetCharacter")] //The Custom Scripting Event node to receive the Event. Add "On" to the node title as an Event naming convention.
    [UnitCategory("Game/Character")]
    public sealed class GetCharacter : Unit
    {
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
        public ValueInput characerID { get; private set; }

        protected override void Definition()
        {
            value      = ValueOutput(nameof(value), Get);//.PredictableIf(IsDefined);
            characerID = ValueInput<int>("customerID", 0);
        }
        
        private Character Get(Flow flow)
        {
            var id = flow.GetValue<int>(this.characerID);

            return CharacterModule.Instance.FindCharacter(id);
        }
    }
}
