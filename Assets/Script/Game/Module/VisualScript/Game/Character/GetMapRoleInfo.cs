using SGame;
using Unity.VisualScripting;
using Unity.Mathematics;
using System.Collections.Generic;
using GameConfigs;
using log4net;
using Sirenix.Utilities;
using UnityEngine;

namespace SGame.VS
{
    /// <summary>
    /// 获取当前地图初始角色信息, 包含 角色类型, 角色数量, 角色集合
    /// </summary>
	[UnitTitle("GetMapRoleInfo")] //The Custom Scripting Event node to receive the Event. Add "On" to the node title as an Event naming convention.
    [UnitCategory("Game/Character")]
    public sealed class GetMapRoleInfo : Unit
    {
        private static ILog log = LogManager.GetLogger("game.character");
        
        [DoNotSerialize]
        public ControlInput     inputTrigger;

        // 成功返回流程
        [DoNotSerialize]
        public ControlOutput    outputSuccess;
        
        [DoNotSerialize]
        public ControlOutput    outputFail;
        
        /// <summary>
        /// 地图ID
        /// </summary>
        [DoNotSerialize]
        public ValueInput mapID { get; private set; }
        
        [DoNotSerialize]
        public ValueInput roleType { get; private set; }
        
        /// <summary>
        /// 角色ID集合
        /// </summary>
        [DoNotSerialize]
        public ValueOutput RoleIDs { get; private set; }
        
        [DoNotSerialize]
        public ValueOutput Weights { get; private set; }
        
        /// <summary>
        /// 角色类型输出
        /// </summary>
        [DoNotSerialize]
        public ValueOutput roleType2 { get; private set; }
        
        /// <summary>
        /// 角色数量
        /// </summary>
        [DoNotSerialize]
        public ValueOutput roleNum { get; private set; }

        private RoleType    _roleType;
        private int         _roleNum;
        private List<int>   _roleIDs = new List<int>();
        private List<int>   _weights = new List<int>();

        protected override void Definition()
        {
            inputTrigger = ControlInput("input", (flow) =>
            {
                _roleType = flow.GetValue<RoleType>(roleType);
                var id = flow.GetValue<int>(mapID);
                _roleIDs.Clear();
                _weights.Clear();

                if (!ConfigSystem.Instance.TryGet(id, out LevelRowData config))
                    return outputFail;

                _roleNum = GetRoleNum(_roleType, config);
                GetRoleIDs(_roleType, config, _roleIDs, _weights);
                return outputSuccess;
            });

            outputSuccess = ControlOutput("Success");
            outputFail    = ControlOutput("Fail");

            mapID       = ValueInput<int>("mapID");
            roleType    = ValueInput<RoleType>("roleType", RoleType.CUSTOMER);
            roleType2   = ValueOutput<RoleType>("roleType", (flow) => _roleType);
            roleNum     = ValueOutput<int>("roleNum", (flow) => _roleNum);
            RoleIDs     = ValueOutput<List<int>>("roleIDs", (flow) => _roleIDs);
            Weights     = ValueOutput<List<int>>("weights", (flow) => _weights);
        }

        /// <summary>
        /// 通过类型获得 创建数量
        /// </summary>
        /// <param name="t"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        static int GetRoleNum(RoleType t, LevelRowData config)
        {
            switch (t)
            {
                case RoleType.CHEF:
                    return config.ChefNum;
                    
                case RoleType.CAR:
                    return config.CarNum;
                
                case RoleType.PLAYER:
                    return 1;
                
                case RoleType.WAITER:
                    return config.WaiterNum;
                
                case RoleType.CUSTOMER:
                    return config.CustomerNum;
            }

            log.Info("Not Found RoleType=" + t);
            return 0;
        }

        static bool GetRoleIDs(RoleType t, LevelRowData config, List<int> roleIds, List<int> weights)
        {
            switch (t)
            {
                case RoleType.CAR:
                    roleIds.AddRange(config.GetCarIdArray());
                    weights.AddRange(config.GetCarWeightArray());
                    return true;
                
                case RoleType.CHEF:
                    roleIds.Add(config.ChefId);
                    weights.Add(100);
                    return true;
                
                case RoleType.PLAYER:
                    roleIds.Add(config.PlayerId);
                    weights.Add(100);
                    return true;
                
                case RoleType.WAITER:
                    roleIds.Add(config.WaiterId);
                    weights.Add(100);
                    return true;
                
                case RoleType.CUSTOMER:
                    roleIds.AddRange(config.GetCustomerIdArray());
                    weights.AddRange(config.GetCustomerWeightArray());
                    return true;
            }
            
            log.Error("Not Found RoleType=" + t);
            return false;
        }
    }
}
