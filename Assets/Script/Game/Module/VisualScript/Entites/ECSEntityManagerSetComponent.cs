using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using SGame.UI;
using Unity.Entities;


namespace SGame.VS
{
    [UnitTitle("SetComponent")] //The Custom Scripting Event node to receive the Event. Add "On" to the node title as an Event naming convention.
    [UnitCategory("Game/ECS")]
    public class ECSEntityManagerSetComponent : Unit
    {
        [DoNotSerialize]
        public ControlInput inputTrigger;

        [DoNotSerialize]
        public ControlOutput outputTrigger;
    
        [DoNotSerialize]
        public ValueInput EntityManager;
        
        [DoNotSerialize]
        public ValueInput Entity;
        
        [DoNotSerialize]
        public ValueInput Value;
        
        
        [DoNotSerialize]
        public ValueOutput OutEntity;

        private Vector2Int result;
        
        //[SerializeAs(nameof(IsAdd))]
        //[Inspectable, UnitHeaderInspectable("IsAdd")]
        //public bool IsAdd;

        /*
        [DoNotSerialize]
        [Inspectable, UnitHeaderInspectable("Arguments")]
        public bool IsAdd
        {
            get => _IsAdd;
            set => _IsAdd = value;
        }
        */
        
        // 端口定义
        protected override void Definition()
        {
            EntityManager   = ValueInput<EntityManager>(nameof(EntityManager));
            Entity          = ValueInput<Vector2Int>(nameof(Entity));
            Value           = ValueInput<IComponentData>(nameof(Value));
            OutEntity        = ValueOutput<Vector2Int>("Entity", (flow)=>result);
            
            inputTrigger = ControlInput("input", (flow) =>
            {
                //Making the resultValue equal to the input value from myValueA concatenating it with myValueB.
                var mgr = flow.GetValue<EntityManager>(EntityManager);
                result  = flow.GetValue<Vector2Int>(Entity);
                var e     = new Entity() {Index =  result.x, Version = result.y};
                var value = flow.GetValue<IComponentData>(Value);
                if (!mgr.HasComponent(e, value.GetType()))
                {
                    mgr.AddComponent(e, value.GetType());
                }
                mgr.SetComponentData(e, value);
                return outputTrigger;
            });
            
            
            outputTrigger = ControlOutput("output");
        }
    }
}