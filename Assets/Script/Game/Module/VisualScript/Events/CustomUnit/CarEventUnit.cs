using Unity.VisualScripting;
using System;
using UnityEngine;

namespace SGame.VS
{
    /// <summary>
    /// 角色初始化事件
    /// </summary>
    [UnitCategory("Events/Game/Character")]
    [UnitTitle("CarInit")]
    public sealed class CarInit : GameObjectEventUnit<CarMono>
    {
        public static string EventHook = "CharacterInit";
    
        protected override string hookName => EventHook;
        
        [DoNotSerialize]
        public ValueOutput characterObject { get; private set; }
        
        protected override void Definition()
        {
            base.Definition();
            characterObject = ValueOutput<CarMono>(nameof(CarMono));
        }
        
        protected override void AssignArguments(Flow flow, CarMono args)
        {
            flow.SetValue(characterObject, args);
        }

        public override Type MessageListenerType { get; }
    }
}