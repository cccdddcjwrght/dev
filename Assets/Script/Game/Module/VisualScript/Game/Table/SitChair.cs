using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using SGame.UI;
using Unity.Entities;

namespace SGame.VS
{
    // 坐下
    [UnitTitle("SitChair")] 
    [UnitCategory("Game/Table")]
    public class SitChair : Unit
    {
        [DoNotSerialize]
        public ControlInput inputTrigger;

        // 成功返回流程
        [DoNotSerialize]
        public ControlOutput    output;
        
        [DoNotSerialize]
        public ValueOutput      result;
        
        [DoNotSerialize]
        public ValueOutput       _out_chair;
        
        [DoNotSerialize]
        public ValueInput       _chair;
        
        [DoNotSerialize]
        public ValueInput       _customID;
        
        private bool            resultValue;
        private ChairData       resultChair;
        
        // 端口定义
        protected override void Definition()
        {
            inputTrigger = ControlInput("Input", (flow) =>
            {
                // Making the resultValue equal to the input value from myValueA concatenating it with myValueB.
                var chair = flow.GetValue<ChairData>(_chair);
                var customID = flow.GetValue<int>(_customID);
                
                resultValue = TableManager.Instance.SitChair(chair, customID);
                resultChair = chair;
                return output;
            });
            
            _chair = ValueInput<ChairData>("chair", ChairData.Empty);
            _customID = ValueInput<int>("custom", 0);
            output = ControlOutput("Output");
            result = ValueOutput<bool>("success", (flow) => resultValue);
            _out_chair = ValueOutput<ChairData>("chair", (flow) => resultChair);
        }
    }
}