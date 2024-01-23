using Unity.VisualScripting;
using System;

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
}