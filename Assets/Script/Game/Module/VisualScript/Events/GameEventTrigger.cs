using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using System;
using System.Linq;
/// 游戏内事件关联到 VS 中区
namespace SGame.VS
{
    [UnitCategory("Events/Game")]
    public class GameEventTrigger : Unit
    {
        public static string EventName = "GameEventTrigger";
        
        [DoNotSerialize]
        public ControlInput inputTrigger;

        [DoNotSerialize]
        public ControlOutput outputTrigger;

        [SerializeAs(nameof(argumentCount))]
        private int _argumentCount;

        [DoNotSerialize]
        [Inspectable, UnitHeaderInspectable("Arguments")]
        public int argumentCount
        {
            get => _argumentCount;
            set => _argumentCount = Mathf.Clamp(value, 0, 5);
        }
        
        /// <summary>
        /// The name of the event.
        /// </summary>
        [DoNotSerialize]
        [PortLabelHidden]
        public ValueInput name { get; private set; }
        
        [DoNotSerialize]
        public List<ValueInput> arguments { get; } = new List<ValueInput>();

        private VSEventBridge.EventType eventHandle;
        
        protected override void Definition()
        {
            // 触发事件
            inputTrigger = ControlInput("inputTrigger", (flow) =>
            {
                var eventId = flow.GetValue<SGame.GameEvent>(name);
                if (eventId != SGame.GameEvent.NONE)
                {
                    object[] args = arguments.Select(v => flow.GetValue<object>(v)).ToArray();
                    EventManager.Instance.TriggerGeneral((int)eventId, args);
                }
                
                return outputTrigger;
            });

            name = ValueInput<SGame.GameEvent>("GameEventID", SGame.GameEvent.NONE);
            arguments.Clear();
            for (int i = 0; i < _argumentCount; i++)
            {
                arguments.Add(ValueInput<object>("param_" + i));
            }
            
            outputTrigger = ControlOutput("outputTrigger");
        }
    }
}