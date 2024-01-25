using SGame;
using Unity.VisualScripting;

namespace SGame.VS
{
    /// <summary>
    /// 销毁角色
    /// </summary>
	[UnitTitle("DestoryCharacter")] 
    [UnitCategory("Game/Character")]
    public sealed class DestoryCharacter : Unit
    {
        [DoNotSerialize]
        public ControlInput     input;

        // 成功返回流程
        [DoNotSerialize]
        public ControlOutput    output;
        
        /// <summary>
        /// The value to return if the variable is not defined.
        /// </summary>
        [DoNotSerialize]
        public ValueInput _characerID { get; private set; }
        
        /// <summary>
        /// The value to return if the variable is not defined.
        /// </summary>
        [DoNotSerialize]
        public ValueInput _character { get; private set; }

        protected override void Definition()
        {
            input = ControlInput("Input", (flow) =>
            {
                var character = flow.GetValue<Character>(_character);
                var customID = flow.GetValue<int>(_characerID);

                if (customID != 0)
                {
                    CharacterModule.Instance.DespawnCharacter(customID);
                }

                if (character != null)
                {
                    CharacterModule.Instance.DespawnCharacter(character.CharacterID);
                }

                return output;
            });

            output          = ControlOutput("Output");
            _characerID     = ValueInput<int>("customerID", 0);
            _character      = ValueInput<Character>("customer", null);
        }
    }
}
