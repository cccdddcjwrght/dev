using SGame;
using Unity.VisualScripting;
using Unity.Mathematics;
using System.Collections.Generic;
using UnityEngine;

namespace SGame.VS
{
    /// <summary>
    /// 获得当前关卡ID
    /// </summary>
	
	[UnitTitle("GetCurrentLevelID")] //The Custom Scripting Event node to receive the Event. Add "On" to the node title as an Event naming convention.
    [UnitCategory("Game/Map")]
    public sealed class GetCurrentLevelID : Unit
    {
        /// <summary>
        /// The value of the variable.
        /// </summary>
        [DoNotSerialize]
        public ValueOutput levelID { get; private set; }
        
        protected override void Definition()
        {
            levelID = ValueOutput<int>("levelID", (flow) => DataCenter.Instance.GetUserData().scene);
        }
    }
}
