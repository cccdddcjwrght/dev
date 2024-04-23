using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using SGame.UI;
using Unity.Entities;
using Unity.Mathematics;

//UI位置设置
namespace SGame.VS
{
    [UnitTitle("UIGetUIPos")]
    [UnitCategory("Game/UI")]
    public class UIGetUIPos : Unit
    {
        [DoNotSerialize]
        public ControlInput inputTrigger;

        [DoNotSerialize]
        public ControlOutput outputTrigger;

        //ui名称
        [DoNotSerialize]
        public ValueInput uiName;
        
        //定位名称
        [DoNotSerialize]
        public ValueInput uiClickName;

        [DoNotSerialize]
        public ValueInput uiCenter;

        [DoNotSerialize]
        public ValueOutput uiPos;
        
        private Vector2 uiValue;

        // 端口定义
        protected override void Definition()
        {
            // 打开UI
            inputTrigger = ControlInput("Input", (flow) =>
            {
                string name1 = flow.GetValue<string>(uiName);
                string name2 = flow.GetValue<string>(uiClickName);
                bool isCenter = flow.GetValue<bool>(uiCenter);

                uiValue= UIUtils.GetUIPosition(name1, name2, isCenter);
                return outputTrigger;
            });
            
            uiName = ValueInput<string>("uiname");
            uiClickName = ValueInput<string>("uiClickName");
            uiCenter = ValueInput<bool>("uiCenter", false);
            uiPos = ValueOutput<Vector2>("UIPos",(flow) => uiValue);
            outputTrigger = ControlOutput("Output");
        }
    }
}