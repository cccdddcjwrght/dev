using log4net;
using SGame;
using Unity.VisualScripting;
using Unity.Mathematics;
using UnityEngine;
using System.Collections.Generic;

namespace SGame.VS
{
    /// <summary>
    /// 播放声音
    /// </summary>
	
	[UnitTitle("PlayAudio")] //The Custom Scripting Event node to receive the Event. Add "On" to the node title as an Event naming convention.
    [UnitCategory("Game/Audio")]
    public sealed class PlayAudio : Unit
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
        public ValueInput AudioID { get; private set; }
        
        /// <summary>
        /// 3D位置信息
        /// </summary>
        [DoNotSerialize]
        public ValueInput mapPos { get; private set; }

        
        protected override void Definition()
        {
            inputTrigger = ControlInput("Input", (flow) =>
            {
                var id =  flow.GetValue<int>(AudioID);
                var pos = flow.GetValue<Vector3>(mapPos);

                log.Info("audio play id=" + id.ToString());
                AudioSystem.Instance.Play(id);
                return outputTrigger;
            });
            
            AudioID      = ValueInput<int>("AudioID", 0);
            mapPos      = ValueInput<Vector3>("mapPos",Vector3.zero);
            outputTrigger = ControlOutput("Output");
        }
    }
}
