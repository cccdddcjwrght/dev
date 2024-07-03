using System.Collections.Generic;
using Unity.VisualScripting;
namespace SGame.VS
{
    [UnitTitle("FindIdleCharacter")] // 查找点单工人
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
        /// 角色 MASK
        /// </summary>
        [DoNotSerialize]
        public ValueInput m_roleTypeMask { get; private set; }
        
        /// <summary>
        /// 工作quyu
        /// </summary>
        [DoNotSerialize]
        public ValueInput m_workerArea { get; private set; }

        [DoNotSerialize]
        public ControlInput m_input;

        [DoNotSerialize]
        public ControlOutput m_output;

        private List<Character> m_result = new List<Character>();
        private AotList m_resultObject = new AotList();

        protected override void Definition()
        {
            m_roleTypeMask = ValueInput<int>("roleTypeMask", 5);
            //m_roleType2 = ValueInput<int>("roleType2", 0);
            m_workerArea = ValueInput<int>("workerArea", 0);
            value       = ValueOutput<AotList>("value", (flow)=> m_resultObject);

            m_input = ControlInput("Input", (flow) =>
            {
                int roleTypeMask = flow.GetValue<int>(m_roleTypeMask);
                //int roleType2 = flow.GetValue<int>(m_roleType2);
                int area = flow.GetValue<int>(m_workerArea);

                CharacterModule.Instance.FindCharacters(m_result, (character) =>
                    BitOperator.Get(character.workerAread, area)              // 区域匹配
                    && character.isIdle                                                     // 状态匹配
                    && BitOperator.Get(roleTypeMask, character.roleType));             // (character.roleType == roleType1 || character.roleType == roleType2));

                m_resultObject.Clear();
                foreach (var item in m_result)
                    m_resultObject.Add(item);

                return m_output;
            });

            m_output = ControlOutput("Output");
        }
    }
}
