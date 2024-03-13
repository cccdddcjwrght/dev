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
    [UnitTitle("UIGetPos")]
    [UnitCategory("Game/UI")]
    public class UIGetPos : Unit
    {
        [DoNotSerialize]
        public ControlInput inputTrigger;

        [DoNotSerialize]
        public ControlOutput outputTrigger;

        //ui
        [DoNotSerialize]
        public ValueInput intXY;

        [DoNotSerialize]
        public ValueOutput uiPos;
        
        private Vector2 uiValue;

        // 端口定义
        protected override void Definition()
        {
            // 打开UI
            inputTrigger = ControlInput("Input", (flow) =>
            {
                int2 xy = flow.GetValue<int2>(intXY);
                Vector3 pos= GameTools.MapAgent.CellToVector(xy.x, xy.y);
                uiValue = UIUtils.WorldPosToUI(pos);
                return outputTrigger;
            });
            
            intXY = ValueInput<int2>("xy");
            uiPos = ValueOutput<Vector2>("UIPos",(flow) => uiValue);
            outputTrigger = ControlOutput("Output");
        }
    }
}