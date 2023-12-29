using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using SGame.UI;
using Unity.Entities;

namespace SGame.VS
{
    public enum PARAM_TYPE : int
    {
        Entity,
        Name
    }
    
    [UnitTitle("UIClose")] //The Custom Scripting Event node to receive the Event. Add "On" to the node title as an Event naming convention.
    [UnitCategory("Game/UI")]
    public class UIClose : Unit
    {
        [DoNotSerialize]
        public ControlInput inputTrigger;

        [DoNotSerialize]
        public ControlOutput outputTrigger;

        //[DoNotSerialize]
        [SerializeAs(nameof(ParamType))]
        [Inspectable, UnitHeaderInspectable("Type")]
        public PARAM_TYPE ParamType;
        
        [DoNotSerialize]
        public ValueInput Entity;
        
        [DoNotSerialize]
        public ValueInput uiName;
        

        // 端口定义
        protected override void Definition()
        {
            // 关闭UI
            inputTrigger = ControlInput("input", (flow) =>
            {
                //Making the resultValue equal to the input value from myValueA concatenating it with myValueB.
                EntityManager mgr = World.DefaultGameObjectInjectionWorld.EntityManager;

                if (ParamType == PARAM_TYPE.Entity)
                {
                    var v = flow.GetValue<Vector2Int>(Entity);
                    var ui = new Entity() { Index = v.x, Version = v.y };
                    UIUtils.CloseUI(ui);
                }
                else
                {
                    var name = flow.GetValue<string>(uiName);
                    UIUtils.CloseUIByName(name);
                }
                
               return outputTrigger;
            });

            if (ParamType == PARAM_TYPE.Entity)
                Entity = ValueInput<Vector2Int>(nameof(Entity), Vector2Int.zero);
            else
                uiName = ValueInput<string>("Name", "");
            outputTrigger = ControlOutput("output");
            
        }
    }
}