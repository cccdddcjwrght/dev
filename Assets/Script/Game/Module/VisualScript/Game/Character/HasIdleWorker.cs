using System.Collections.Generic;
using Unity.VisualScripting;
namespace SGame.VS
{
    [UnitTitle("HasIdleWorker")] // 查找点单工人
    [UnitCategory("Game/Character")]
    public sealed class HasIdleWorker : Unit
    {
        /// <summary>
        /// The value of the variable.
        /// </summary>
        [DoNotSerialize]
        [PortLabelHidden]
        public ValueOutput value { get; private set; }

        /// <summary>
        /// 角色 MASK
        /// </summary>
        [DoNotSerialize]
        public ValueInput m_roleTypeMask { get; private set; }
        
        [DoNotSerialize]
        public ControlInput m_input;

        [DoNotSerialize]
        public ControlOutput m_yes;
        
        [DoNotSerialize]
        public ControlOutput m_no;

        private List<Character> m_cache = new List<Character>();

        protected override void Definition()
        {
            m_roleTypeMask = ValueInput<int>("roleTypeMask", 5);

            m_input = ControlInput("Input", (flow) =>
            {
                int roleTypeMask = flow.GetValue<int>(m_roleTypeMask);

                CharacterModule.Instance.FindCharacters(m_cache, (character) => character.isIdle && BitOperator.Get(roleTypeMask, character.roleType));             // (character.roleType == roleType1 || character.roleType == roleType2));

                return m_cache.Count > 0 ? m_yes : m_no;
            });

            m_yes   = ControlOutput("yes");
            m_no      = ControlOutput("no");
        }
    }
}
