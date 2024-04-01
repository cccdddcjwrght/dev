using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using SGame.UI;
using Unity.Entities;

namespace SGame.VS
{
    [UnitTitle("UIGuideCreate")] //The Custom Scripting Event node to receive the Event. Add "On" to the node title as an Event naming convention.
    [UnitCategory("Game/UI")]
    public class UIGuideCreate : Unit
    {
        [DoNotSerialize]
        public ControlInput inputTrigger;

        [DoNotSerialize]
        public ControlOutput outputTrigger;
        
        [DoNotSerialize]
        public ValueInput uiName;
        
        [DoNotSerialize]
        public ValueInput uiMaskName;
        
        //UI位置
        [DoNotSerialize]
        public ValueInput uiPos;
        
        //UI大小
        [DoNotSerialize]
        public ValueInput uiSize;
        
        //UI透明度
        [DoNotSerialize]
        public ValueInput uiAlpha;

        [DoNotSerialize]
        public ValueOutput uiResult;
        
        [DoNotSerialize]
        public ValueOutput uiMaskResult;
        
        private Vector2Int resultValue;
        private Vector2Int resultmaskValue;
        
        // 端口定义
        protected override void Definition()
        {
            // 打开UI
            inputTrigger = ControlInput("Input", (flow) =>
            {

                EntityManager mgr = World.DefaultGameObjectInjectionWorld.EntityManager;
                Vector2Int sizeint2 = new Vector2Int(0, 0);
                Vector2Int s2 = flow.GetValue<Vector2Int>(uiSize);
                //遮罩
                if (s2!=sizeint2)
                {
                    Entity uiMask = UIRequest.Create(mgr, UIUtils.GetUI(flow.GetValue<string>(uiMaskName)));
                    mgr.AddComponentObject(uiMask, new UISize() { size = s2 });
                    Vector2 p2 = flow.GetValue<Vector2>(uiPos);
                    float a2= flow.GetValue<float>(uiAlpha);
                    mgr.AddComponentObject(uiMask, new UIPos() { pos = p2 });
                    mgr.AddComponentObject(uiMask, new UIAlpha() { alpha = a2 });
                    resultmaskValue=new Vector2Int(uiMask.Index, uiMask.Version);
                }
                //手指
                if (flow.GetValue<string>(uiName) != string.Empty) 
                {
                    Entity ui = UIRequest.Create(mgr, UIUtils.GetUI(flow.GetValue<string>(uiName)));
                    Vector2 p3 = flow.GetValue<Vector2>(uiPos);
                    mgr.AddComponentObject(ui, new UIPos() { pos = p3 });
                    resultValue = new Vector2Int(ui.Index, ui.Version);
                }
                return outputTrigger;
            });
            
            uiName  = ValueInput<string>("UIName", "");
            uiMaskName  = ValueInput<string>("UIMaskName", "");
            uiPos  = ValueInput<Vector2>("UIPos");
            uiSize = ValueInput<Vector2Int>("UISize");
            uiAlpha = ValueInput<float>("UIAlpha");
            outputTrigger = ControlOutput("Output");
            uiResult = ValueOutput<Vector2Int>("UI Entity", (flow) => resultValue);
            uiMaskResult= ValueOutput<Vector2Int>("UI Mask Entity", (flow) => resultmaskValue);
        }
    }
}