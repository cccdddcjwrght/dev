using SGame;
using Unity.VisualScripting;
using Unity.Mathematics;
using System.Collections.Generic;
using FlatBuffers;
using GameConfigs;
using log4net;
using Sirenix.Utilities;
using UnityEngine;

namespace SGame.VS
{
    /// <summary>
    /// 获得关卡食物的权重信息
    /// </summary>
	[UnitTitle("GetLevelFoodWidgets")] //The Custom Scripting Event node to receive the Event. Add "On" to the node title as an Event naming convention.
    [UnitCategory("Game/Map")]
    public sealed class GetLevelFoodWidgets : Unit
    {
        private static ILog log = LogManager.GetLogger("game.map");

        [DoNotSerialize]
        public ControlInput inputTrigger;
        
        [DoNotSerialize]
        public ControlOutput outputTrigger;
            
        [DoNotSerialize]
        public ControlOutput failTrigger;

        [DoNotSerialize]
        public ValueInput m_levelID;
        
        /// <summary>
        /// 已开启的食物
        /// </summary>
        [DoNotSerialize]
        public ValueOutput m_foodTypes { get; private set; }
        
        /// <summary>
        /// 食物的权重
        /// </summary>
        [DoNotSerialize]
        public ValueOutput m_foodWidgets { get; private set; }
        

        private int _foodType;

        private List<int> _foodTypes;

        private List<int> _foodWidgets;
        
        protected override void Definition()
        {
            _foodTypes = new List<int>();
            _foodWidgets = new List<int>();
            m_levelID = ValueInput<int>("levelID");
            m_foodTypes = ValueOutput<List<int>>("foodType", (flow) => _foodTypes);
            m_foodWidgets = ValueOutput<List<int>>("foodWidgets", (flow) => _foodWidgets);
            outputTrigger = ControlOutput("success");
            failTrigger = ControlOutput("fail");

            inputTrigger = ControlInput("input", (flow) =>
            {
                _foodTypes.Clear();
                _foodWidgets.Clear();
                var machines = TableManager.Instance.GetOpenMachineIDs();
                var foods = TableManager.Instance.GetOpenFoodTypes();

                if (foods.Count <= 0)
                    return failTrigger;
                
                var id = flow.GetValue<int>(m_levelID);
                if (!ConfigSystem.Instance.TryGet(id, out LevelRowData config))
                {
                    log.Error("level id not found=" + id);
                    return failTrigger;
                }

                if (config.MachineIdLength != config.OrderWeightLength || config.MachineIdLength <= 0)
                {
                    log.Error("machineid length not match =" + config.MachineIdLength + ":" + config.OrderWeightLength);
                    return failTrigger;
                }



                var len = config.MachineIdLength;
                for (int i = 0; i < config.MachineIdLength; i++)
                {
                    int machineID = config.MachineId(i);
                    int index = machines.IndexOf(machineID);
                    if (index >= 0)
                    {
                        _foodTypes.Add(foods[index]);
                        _foodWidgets.Add(config.OrderWeight(i));
                    }
                }

                if (_foodTypes.Count <= 0)
                {
                    log.Warn("Level Config OrderWeight Error Food Not Found!");
                    return failTrigger;
                }
                return outputTrigger;
            });
        }
    }
}
