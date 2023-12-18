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
        public ValueOutput result;
        
        private Vector2Int resultValue;
        
        // 端口定义
        protected override void Definition()
        {
            // 打开UI
            inputTrigger = ControlInput("inputTrigger", (flow) =>
            {
                //Making the resultValue equal to the input value from myValueA concatenating it with myValueB.
                EntityManager mgr = World.DefaultGameObjectInjectionWorld.EntityManager;
                Entity ui = UIRequest.Create(mgr, UIUtils.GetUI(flow.GetValue<string>(uiName)));
                resultValue = new Vector2Int(ui.Index, ui.Version);
                return outputTrigger;
            });
            
            uiName = ValueInput<string>("UIName", "");
            outputTrigger = ControlOutput("outputTrigger");
            result = ValueOutput<Vector2Int>("UI Entity", (flow) => resultValue);
        }
    }
}