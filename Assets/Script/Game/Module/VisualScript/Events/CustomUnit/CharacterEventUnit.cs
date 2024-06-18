using Unity.VisualScripting;
using System;
using UnityEngine;

namespace SGame.VS
{
    /// <summary>
    /// 角色初始化事件
    /// </summary>
    [UnitCategory("Events/Game/Character")]
    [UnitTitle("CharacterInit")]
    public sealed class CharacterInit : GameObjectEventUnit<Character>
    {
        public static string EventHook = "CharacterInit";
    
        protected override string hookName => EventHook;
        
        [DoNotSerialize]
        public ValueOutput characterObject { get; private set; }
        
        protected override void Definition()
        {
            base.Definition();
            characterObject = ValueOutput<Character>(nameof(Character));
        }
        
        protected override void AssignArguments(Flow flow, Character args)
        {
            flow.SetValue(characterObject, args);
        }

        public override Type MessageListenerType { get; }
    }
    
    /// <summary>
    /// 发送角色初始化事件
    /// </summary
    [UnitTitle("SendCharacterInit")]
    [UnitCategory("Events/Game/Character")] //Setting the path to find the node in the fuzzy finder as Events > My Events.
    public class SendCharacterInit : Unit
    {
        [DoNotSerialize]  // Mandatory attribute, to make sure we don’t serialize data that should never be serialized.
        [PortLabelHidden] // Hide the port label, as we normally hide the label for default Input and Output triggers.
        public ControlInput inputTrigger { get; private set; }
        [DoNotSerialize]
        public ValueInput character;
        [DoNotSerialize]
        public ValueInput script;

        [PortLabelHidden] // Hide the port label, as we normally hide the label for default Input and Output triggers.
        public ControlOutput outputTrigger { get; private set; }

        protected override void Definition()
        {
            inputTrigger = ControlInput(nameof(inputTrigger), Trigger);
            character = ValueInput<Character>(nameof(character));
            script  = ValueInput<GameObject>(nameof(script));
            outputTrigger = ControlOutput(nameof(outputTrigger));
            Succession(inputTrigger, outputTrigger);
        }

        //Send the Event MyCustomEvent with the integer value from the ValueInput port myValueA.
        private ControlOutput Trigger(Flow flow)
        {
            EventBus.Trigger(CharacterInit.EventHook, flow.GetValue<GameObject>(script), flow.GetValue<Character>(character));
            return outputTrigger;
        }
    }
}