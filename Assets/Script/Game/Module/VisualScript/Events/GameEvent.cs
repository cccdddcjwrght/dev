using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using System;
/// 游戏内事件关联到 VS 中区
namespace SGame.VS
{
    [UnitCategory("Events/Game")]
    public class GameEvent : EventUnit<CustomEventArgs>
    {
        public static string EventHOOK = "GameEvent";
        protected override bool register => false;

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

        private SGame.GameEvent _eventId;

        [DoNotSerialize]
        public List<ValueOutput> argumentPorts { get; } = new List<ValueOutput>();

        private VSEventBridge.EventType eventHandle;

        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook(EventHOOK);
        }

        protected override void Definition()
        {
            base.Definition();

            name = ValueInput(nameof(name), SGame.GameEvent.NONE);

            argumentPorts.Clear();

            for (var i = 0; i < argumentCount; i++)
            {
                argumentPorts.Add(ValueOutput<object>("argument_" + i));
            }
        }
        
        protected override bool ShouldTrigger(Flow flow, CustomEventArgs args)
        {
            //var eventID = flow.GetValue<SGame.GameEvent>(name);
            //return CompareNames(flow, name, args.name);
            return true;
        }
        
        
        // Setting the value on our port.
        protected override void AssignArguments(Flow flow, CustomEventArgs args)
        {
            for (var i = 0; i < argumentCount; i++)
            {
                flow.SetValue(argumentPorts[i], args.arguments[i]);
            }
        }
        
        public override void StartListening(GraphStack stack)
        {
            var data = stack.GetElementData<Data>(this);
            if (data.isListening)
            {
                return;
            }
            
            var reference = stack.ToReference();
            var flow = Flow.New(reference);
            _eventId = flow.GetValue<SGame.GameEvent>(name);
            flow.Dispose();
            eventHandle = (args) => OnGameEvent(reference, args);
            VSEventBridge.Instance.Reg((int)_eventId, eventHandle);
            
            base.StartListening(stack);
        }
        
        public virtual void StopListening(GraphStack stack)
        {
            var data = stack.GetElementData<Data>(this);
            if (!data.isListening)
            {
                return;
            }
            base.StopListening(stack);

            if (eventHandle != null)
            {
                VSEventBridge.Instance.UnReg((int)_eventId, eventHandle);
                eventHandle = null;
            }
        }
        
        /// <summary>
        /// 触发事件
        /// </summary>
        /// <param name="reference"></param>
        /// <param name="args"></param>
        public void OnGameEvent(GraphReference reference, params object[] args)
        {
            CustomEventArgs eventArgs = new CustomEventArgs(_eventId.ToString(), args);
            Trigger(reference, eventArgs);
        }
    }
}