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
    /// 随机数据ID内容
    /// </summary>
	[UnitTitle("GetRandomID")] //The Custom Scripting Event node to receive the Event. Add "On" to the node title as an Event naming convention.
    [UnitCategory("Game/Random")]
    public sealed class GetRandomID : Unit
    {
        private static ILog log = LogManager.GetLogger("game.random");
        
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
        public ValueInput IDs { get; private set; }
        
        // 权重
        [DoNotSerialize]
        public ValueInput Weights { get; private set; }
        
        // 输出数量
        [DoNotSerialize]
        public ValueInput Num       { get; private set; }
        
        // 是否重复
        [DoNotSerialize]
        public ValueInput IsRepeate { get; private set; }

        [DoNotSerialize]
        public ValueOutput outIDs;

        [DoNotSerialize]
        public ValueOutput firstID;

        // 数据数据
        private List<int> _outIDS;


        protected override void Definition()
        {
            _outIDS = new List<int>();
            inputTrigger = ControlInput("input", (flow) =>
            {
                _outIDS.Clear();
                
                var ids = flow.GetValue<List<int>>(IDs);
                var weights = flow.GetValue<List<int>>(Weights);
                var num = flow.GetValue<int>(Num);
                if (ids == null || ids.Count == 0)
                {
                    log.Error("input role ids is null or zero");
                    return outputFail;
                }
                if (weights != null && weights.Count != ids.Count)
                {
                    log.Error("weight not match");
                    return outputFail;
                }
                
                // 计算随机数
                if (num > 1)
                {
                    if (RandomSystem.Instance.GetRandomIDs(ids, weights, num, _outIDS))
                        return outputSuccess;
                }
                else
                {
                    _outIDS.Add(RandomSystem.Instance.GetRandomID(ids, weights));
                }

                return outputFail;
            });

            outputSuccess   = ControlOutput("Success");
            outputFail      = ControlOutput("Fail");
            Num             = ValueInput<int>("Num", 1);
            IDs             = ValueInput<List<int>>("IDs");
            Weights         = ValueInput<List<int>>("weights", null);
            outIDs          = ValueOutput<List<int>>("IDs", (flow) => _outIDS);
            firstID         = ValueOutput<int>("firstID", (flow) => _outIDS[0]);
        }
    }
}
