using SGame;
using Unity.VisualScripting;
using Unity.Mathematics;
using System.Collections.Generic;

namespace SGame.VS
{
    /// <summary>
    /// 通过标签获得地图位置信息
    /// </summary>
	
	[UnitTitle("GetTagPos")] //The Custom Scripting Event node to receive the Event. Add "On" to the node title as an Event naming convention.
    [UnitCategory("Game/Map")]
    public sealed class GetTagPos : Unit
    {
        [DoNotSerialize]
        public ControlInput     inputTrigger;

        // 成功返回流程
        [DoNotSerialize]
        public ControlOutput    outputSuccess;
        
        [DoNotSerialize]
        public ControlOutput    outputFail;
        
        /// <summary>
        /// The value of the variable.
        /// </summary>
        [DoNotSerialize]
        public ValueOutput firstValue { get; private set; }
        
        /// <summary>
        /// The value of the variable.
        /// </summary>
        [DoNotSerialize]
        public ValueOutput Values { get; private set; }

        /// <summary>
        /// 地图 上的标签
        /// </summary>
        [DoNotSerialize]
        public ValueInput tag { get; private set; }
        
        /// <summary>
        /// 是否使用随机
        /// </summary>
        public ValueInput isRandom { get; private set; }

        
        private List<int2> _mapPosList;

        private bool        _isRandom;
        

        protected override void Definition()
        {
            inputTrigger = ControlInput("input", (flow) =>
            {
                var t = flow.GetValue<string>(tag);
                _isRandom = flow.GetValue<bool>(isRandom);
                if (string.IsNullOrEmpty(t))
                    return outputFail;


                var datas = GameTools.MapAgent.GetTagGrids(t);
                if (datas == null || datas.Count == 0)
                    return outputFail;

                _mapPosList = new List<int2>(datas.Count);
                foreach (var v in datas)
                {
                    _mapPosList.Add(new int2(v.x, v.y));
                }
                return outputSuccess;
            });
            
            //RandomSystem.Instance.NextInt()
            
            Values      = ValueOutput(nameof(Values), (flow) =>  _mapPosList);
            firstValue  = ValueOutput(nameof(firstValue), (flow) =>
            {
                if (_mapPosList == null || _mapPosList.Count == 0)
                    return int2.zero;

                if (_isRandom)
                    return _mapPosList[RandomSystem.Instance.NextInt(0, _mapPosList.Count)];

                return _mapPosList[0];
            });
            
            tag             = ValueInput<string>("tag", "");
            isRandom        = ValueInput<bool>("isRandom", true);
            outputSuccess   = ControlOutput("success");
            outputFail      = ControlOutput("fail");        
        }
    }
}
