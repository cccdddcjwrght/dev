using SGame;
using Unity.VisualScripting;
using System.Collections.Generic;

namespace SGame.VS
{
    /// <summary>
    /// 获得开启的食物类型
    /// </summary>
	
	[UnitTitle("GetOpenFoodTypes")] //The Custom Scripting Event node to receive the Event. Add "On" to the node title as an Event naming convention.
    [UnitCategory("Game/Table")]
    public sealed class GetOpenFoodTypes : Unit
    {
        /// <summary>
        /// The value of the variable.
        /// </summary>
        [DoNotSerialize]
        [PortLabelHidden]
        public ValueOutput foodTypes { get; private set; }
        
        protected override void Definition()
        {
            foodTypes      = ValueOutput(nameof(foodTypes), (flow) => TableManager.Instance.GetOpenFoodTypes());
        }
        
    }
}
