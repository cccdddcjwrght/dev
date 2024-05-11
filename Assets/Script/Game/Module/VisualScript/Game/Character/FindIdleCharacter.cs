using System.Collections.Generic;
using Unity.VisualScripting;
namespace SGame.VS
{
    [UnitTitle("FindIdleCharacter")] // 返回某个角色类型的角色
    [UnitCategory("Game/Character")]
    public sealed class FindIdleCharacter : Unit
    {
        /// <summary>
        /// The value of the variable.
        /// </summary>
        [DoNotSerialize]
        [PortLabelHidden]
        public ValueOutput value { get; private set; }

        /// <summary>
        /// The value to return if the variable is not defined.
        /// </summary>
        [DoNotSerialize]
        public ValueInput m_roleType1 { get; private set; }
        
        /// <summary>
        /// The value to return if the variable is not defined.
        /// </summary>
        [DoNotSerialize]
        public ValueInput m_roleType2 { get; private set; }

        [DoNotSerialize]
        public ControlInput m_input;

        [DoNotSerialize]
        public ControlOutput m_output;

        private List<Character> m_result = new List<Character>();

        protected override void Definition()
        {
            m_roleType1 = ValueInput<int>("roleType1", 5);
            m_roleType2 = ValueInput<int>("roleType2", 0);
            value       = ValueOutput<List<Character>>("value", (flow)=>m_result);

            m_input = ControlInput("Input", (flow) =>
            {
                int roleType1 = flow.GetValue<int>(m_roleType1);
                int roleType2 = flow.GetValue<int>(m_roleType2);

                m_result = CharacterModule.Instance.FindCharacters((character) => character.isIdle
                    && (character.roleType == roleType1 || character.roleType == roleType2));

                return m_output;
            });

            m_output = ControlOutput("Output");
        }
    }
}
