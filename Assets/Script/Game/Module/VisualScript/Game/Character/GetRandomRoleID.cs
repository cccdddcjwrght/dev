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
	[UnitTitle("GetRandomRoleID")] //The Custom Scripting Event node to receive the Event. Add "On" to the node title as an Event naming convention.
    [UnitCategory("Game/Character")]
    public sealed class GetRandomRoleID : Unit
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
        /// 角色ID集合
        /// </summary>
        [DoNotSerialize]
        public ValueInput RoleIDs { get; private set; }
        
        [DoNotSerialize]
        public ValueInput Weights { get; private set; }

        [DoNotSerialize]
        public ValueOutput roleID;

        private int _roleID;
        

        protected override void Definition()
        {
            inputTrigger = ControlInput("input", (flow) =>
            {
                var roleIDs = flow.GetValue<List<int>>(RoleIDs);
                var weights = flow.GetValue<List<int>>(Weights);
                if (roleIDs == null || roleIDs.Count == 0)
                {
                    log.Error("input role ids is null or zero");
                    return outputFail;
                }

                if (weights != null && weights.Count != roleIDs.Count)
                {
                    log.Error("weight not match");
                    return outputFail;
                }
                
                // 计算随机数
                if (weights == null || weights.Count == 0)
                {
                    int randIndex = RandomSystem.Instance.NextInt(0, roleIDs.Count);
                    _roleID = roleIDs[randIndex];
                    return outputSuccess;
                } 
                
                // 计算
                int allCount = 0;
                foreach (var item in weights)
                    allCount += item;

                int randValue = RandomSystem.Instance.NextInt(0, allCount);
                int count = 0;
                for (int i = 0; i < weights.Count; i++)
                {
                    count += weights[i];
                    if (randValue < count)
                    {
                        _roleID = roleIDs[i];
                        return outputSuccess;
                    }
                }
                _roleID = roleIDs[roleIDs.Count - 1];
                return outputSuccess;
            });

            outputSuccess = ControlOutput("Success");
            outputFail    = ControlOutput("Fail");
            RoleIDs       = ValueInput<List<int>>("roleIDs");
            Weights       = ValueInput<List<int>>("weights", null);
            roleID        = ValueOutput<int>("roleID", (flow) => _roleID);
        }
    }
}
