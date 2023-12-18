using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using SGame.UI;
using Unity.Entities;

namespace SGame.VS
{
    [UnitTitle("UIClose")] //The Custom Scripting Event node to receive the Event. Add "On" to the node title as an Event naming convention.
    [UnitCategory("Game/UI")]
    public class UIClose : Unit
    {
        [DoNotSerialize]
        public ControlInput inputTrigger;

        [DoNotSerialize]
        public ControlOutput outputTrigger;
        
        [DoNotSerialize]
        public ValueInput Entity;
        
        //[DoNotSerialize]
        //public ValueOutput EntityOut;

        private Vector2Int result;

        // 端口定义
        protected override void Definition()
        {
            // 关闭UI
            inputTrigger = ControlInput("input", (flow) =>
            {
                //Making the resultValue equal to the input value from myValueA concatenating it with myValueB.
                EntityManager mgr = World.DefaultGameObjectInjectionWorld.EntityManager;
                var v = flow.GetValue<Vector2Int>(Entity);
                var ui = new Entity() { Index = v.x, Version = v.y };
                result = v;
                UIUtils.CloseUI(mgr, ui);
               return outputTrigger;
            });

            Entity = ValueInput<Vector2Int>(nameof(Entity), Vector2Int.zero);
            //EntityOut = ValueOutput<Vector2Int>("UI Entity", (flow) => result);
            outputTrigger = ControlOutput("output");
            
        }
    }
}