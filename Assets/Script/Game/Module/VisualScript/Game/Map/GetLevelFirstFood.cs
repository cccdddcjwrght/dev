using SGame;
using Unity.VisualScripting;
using Unity.Mathematics;
using System.Collections.Generic;
using FlatBuffers;
using GameConfigs;
using log4net;
using UnityEngine;

namespace SGame.VS
{
    /// <summary>
    /// 获得关卡第一个食物ID
    /// </summary>
	[UnitTitle("GetLevelFirstFood")] //The Custom Scripting Event node to receive the Event. Add "On" to the node title as an Event naming convention.
    [UnitCategory("Game/Map")]
    public sealed class GetLevelFirstFood : Unit
    {
        private static ILog log = LogManager.GetLogger("game.map");
        
        [DoNotSerialize]
        public ValueInput m_levelID;
        
        /// <summary>
        /// The value of the variable.
        /// </summary>
        [DoNotSerialize]
        public ValueOutput m_foodType { get; private set; }
        
        protected override void Definition()
        {
            m_levelID = ValueInput<int>("levelID");
            m_foodType = ValueOutput<int>("foodType", (flow) =>
            {
                var id = flow.GetValue<int>(m_levelID);
                if (!ConfigSystem.Instance.TryGet(id, out LevelRowData config))
                {
                    log.Error("level id not found=" + id);
                    return 0;
                }

                if (!ConfigSystem.Instance.TryGet(config.FirstOrder, out MachineRowData mconfig))
                {
                    log.Error("machine id not found=" + config.FirstOrder);
                    return 0;
                }

                return mconfig.ItemId(0);
            });
        }
    }
}
