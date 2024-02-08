using System;
using System.Collections;
using System.Collections.Generic;
using log4net;
using UnityEngine;
using Unity.VisualScripting;
using SGame.UI;
using Unity.Entities;

namespace SGame.VS
{
    // 添加等待服务的座椅
    [UnitTitle("EnqueueChair")] 
    [UnitCategory("Game/Table")]
    public class EnqueueChair : Unit
    {
        private static ILog log = LogManager.GetLogger("game.table");
        
        [DoNotSerialize]
        public ControlInput inputTrigger;

        // 成功返回流程
        [DoNotSerialize]
        public ControlOutput    output;

        [DoNotSerialize]
        public ValueOutput       _out_chair;
        
        [DoNotSerialize]
        public ValueInput       _chair;

        private ChairData       m_resultChair;
        
        // 端口定义
        protected override void Definition()
        {
            inputTrigger = ControlInput("Input", (flow) =>
            {
                // Making the resultValue equal to the input value from myValueA concatenating it with myValueB.
                var chair = flow.GetValue<ChairData>(_chair);
                m_resultChair = chair;
                WaitForServiceChair.Instance.Enqueue(chair);
                return output;
            });
            output = ControlOutput("Output");

            _chair = ValueInput<ChairData>("chair", ChairData.Null);
            _out_chair = ValueOutput<ChairData>("chair", (flow) => m_resultChair);
        }
    }
}
