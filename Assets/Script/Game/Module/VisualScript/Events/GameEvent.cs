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
        
        public new class Data : EventUnit<CustomEventArgs>.Data
        {
            public SGame.GameEvent eventId = SGame.GameEvent.NONE;
            public VSEventBridge.EventType eventHandle;
        }
        
        public override IGraphElementData CreateData()
        {
            return new Data();
        }
        
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
        public ValueInput eventId { get; private set; }
        
        [DoNotSerialize]
        public List<ValueOutput> argumentPorts { get; } = new List<ValueOutput>();

        //private VSEventBridge.EventType eventHandle;

        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook(EventHOOK);
        }

        protected override void Definition()
        {
            base.Definition();

            eventId = ValueInput(nameof(eventId), SGame.GameEvent.NONE);

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
            if (data.eventId != SGame.GameEvent.NONE)
            {
                Debug.LogError("data event repeate!");
                return;
            }
            
            // 注册事件
            data.eventId = Flow.FetchValue<SGame.GameEvent>(eventId, reference);;
            data.eventHandle = (args) => OnGameEvent(reference, args);
            VSEventBridge.Instance.Reg((int)data.eventId, data.eventHandle);
            base.StartListening(stack);
        }
        
        //protected override void StopListening(GraphStack stack, bool destroyed)
        public override void StopListening(GraphStack stack)
        {
            var data = stack.GetElementData<Data>(this);
            if (!data.isListening)
            {
                return;
            }
            base.StopListening(stack);

            if (data.eventId == SGame.GameEvent.NONE)
            {
                return;
            }
            
            VSEventBridge.Instance.UnReg((int)data.eventId, data.eventHandle);
            data.eventId = SGame.GameEvent.NONE;
            data.eventHandle = null;
        }
        
        /// <summary>
        /// 触发事件
        /// </summary>
        /// <param name="reference"></param>
        /// <param name="args"></param>
        public void OnGameEvent(GraphReference reference, params object[] args)
        {
            CustomEventArgs eventArgs = new CustomEventArgs("GameEvent", args);
            Trigger(reference, eventArgs);
        }
    }
}