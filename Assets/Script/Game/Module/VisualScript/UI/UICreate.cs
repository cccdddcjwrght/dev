using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using SGame.UI;
using Unity.Entities;

namespace SGame.VS
{
    [UnitTitle("UICreate")] //The Custom Scripting Event node to receive the Event. Add "On" to the node title as an Event naming convention.
    [UnitCategory("Game/UI")]
    public class UICreate : Unit
    {
        [DoNotSerialize]
        public ControlInput inputTrigger;

        [DoNotSerialize]
        public ControlOutput outputTrigger;
        
        [DoNotSerialize]
        public ValueInput uiName;
        
        [DoNotSerialize]
        public ValueInput uiParam;

        [DoNotSerialize]
        public ValueOutput result;
        
        private Vector2Int resultValue;
        
        // 端口定义
        protected override void Definition()
        {
            // 打开UI
            inputTrigger = ControlInput("Input", (flow) =>
            {
               
                EntityManager mgr = World.DefaultGameObjectInjectionWorld.EntityManager;
                Entity ui = UIRequest.Create(mgr, UIUtils.GetUI(flow.GetValue<string>(uiName)));

                if (uiParam.hasValidConnection)
                {
                    object param = flow.GetValue<object>(uiParam);
                    if (param != null)
                    {
                        mgr.AddComponentObject(ui, new UIParam { Value = param });
                    }
                }

                resultValue = new Vector2Int(ui.Index, ui.Version);
                return outputTrigger;
            });
            
            uiName  = ValueInput<string>("UIName", "");
            uiParam = ValueInput<object>("UIParam", null);
            outputTrigger = ControlOutput("Output");
            result = ValueOutput<Vector2Int>("UI Entity", (flow) => resultValue);
        }
    }
}