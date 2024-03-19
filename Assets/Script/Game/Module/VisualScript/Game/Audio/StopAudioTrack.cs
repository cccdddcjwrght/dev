using log4net;
using SGame;
using Unity.VisualScripting;
using Unity.Mathematics;
using UnityEngine;
using System.Collections.Generic;

namespace SGame.VS
{
    /// <summary>
    /// 停止某个轨道的声音
    /// </summary>
	
	[UnitTitle("StopAudioTrack")] //The Custom Scripting Event node to receive the Event. Add "On" to the node title as an Event naming convention.
    [UnitCategory("Game/Audio")]
    public sealed class StopAudioTrack : Unit
    {
        private static ILog log = LogManager.GetLogger("game.audio");
        
        [DoNotSerialize]
        public ControlInput     inputTrigger;

        // 成功返回流程
        [DoNotSerialize]
        public ControlOutput    outputTrigger;

        /// <summary>
        /// 声音ID
        /// </summary>
        [DoNotSerialize]
        public ValueInput TrackID { get; private set; }


        protected override void Definition()
        {
            inputTrigger = ControlInput("Input", (flow) =>
            {
                var id =  flow.GetValue<int>(TrackID);
                AudioSystem.Instance.Stop((SoundType)id);
                return outputTrigger;
            });
            
            TrackID      = ValueInput<int>("TrackID", 1);
            outputTrigger = ControlOutput("Output");
        }
    }
}
