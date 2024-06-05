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
    /// 获得关卡随机食物
    /// </summary>
	[UnitTitle("GetLevelFoodID")] //The Custom Scripting Event node to receive the Event. Add "On" to the node title as an Event naming convention.
    [UnitCategory("Game/Map")]
    public sealed class GetLevelFoodID : Unit
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

        [DoNotSerialize]
        public ValueInput m_tableID; // 桌子ID
        
        /// <summary>
        /// 已开启的食物
        /// </summary>
        [DoNotSerialize]
        public ValueOutput m_foodID { get; private set; }
        
        private int _foodID;

        private List<int> _foodWidgets = new List<int>();
        private List<int> _foodIDs = new List<int>();

        void GetDefaultOrders(LevelRowData config)
        {
            var machines = TableManager.Instance.GetOpenMachineIDs();
            var foods = TableManager.Instance.GetOpenFoodTypes();
            var widgets = TableManager.Instance.GetMachineWidgets();
            
            for (int i = 0; i < config.OrderMachineIDLength; i++)
            {
                int machineID = config.OrderMachineID(i);
                int index = machines.IndexOf(machineID);
                if (index >= 0)
                {
                    // 改操作台已打开
                    _foodWidgets.Add(widgets[index]);
                    _foodIDs.Add(foods[index]);
                }
            }
        }
        
        protected override void Definition()
        {
            m_levelID       = ValueInput<int>("levelID");
            m_tableID       = ValueInput<int>("tableID");
            outputTrigger   = ControlOutput("success");
            failTrigger     = ControlOutput("fail");
            m_foodID        = ValueOutput<int>("foodID", (flow) => _foodID);

            inputTrigger = ControlInput("input", (flow) =>
            {
                _foodWidgets.Clear();
                _foodIDs.Clear();
                var machines = TableManager.Instance.GetOpenMachineIDs();
                var foods = TableManager.Instance.GetOpenFoodTypes();
                var widgets = TableManager.Instance.GetMachineWidgets();
                var tableID = flow.GetValue<int>(m_tableID);//TableManager.Instance.Get(tableID);
                var table = TableManager.Instance.Get(tableID);
                if (table == null)
                {
                    log.Error("table id not found=" + tableID);
                    return failTrigger;
                }

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

                if (table.roomAreaID <= 1)
                {
                    // 默认区域
                    GetDefaultOrders(config);
                }
                else
                {
                    if (!ConfigSystem.Instance.TryGet(table.roomAreaID, out RoomAreaRowData roomAreaConfig))
                    {
                        log.Error("room area id not found=" + table.roomAreaID);
                        return failTrigger;
                    }
                    
                    // 解锁区域
                    for (int i = 0; i < roomAreaConfig.OrderMachineIDLength; i++)
                    {
                        int machineID = roomAreaConfig.OrderMachineID(i);
                        int index = machines.IndexOf(machineID);
                        if (index >= 0)
                        {
                            // 改操作台已打开
                            _foodWidgets.Add(widgets[index]);
                            _foodIDs.Add(foods[index]);
                        }
                    }

                    if (_foodIDs.Count == 0)
                        GetDefaultOrders(config);
                }

                if (_foodIDs.Count == 0)
                    return failTrigger;

                _foodID = RandomSystem.Instance.GetRandomID(_foodIDs, _foodWidgets);
                return outputTrigger;
            });
        }
    }
}
