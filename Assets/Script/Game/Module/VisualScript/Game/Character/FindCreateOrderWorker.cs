using System.Collections.Generic;
using Unity.VisualScripting;
namespace SGame.VS
{
    [UnitTitle("FindCreateOrderWorker")] // 返回某个角色类型的角色
    [UnitCategory("Game/Character")]
    public sealed class FindCreateOrderWorker : Unit
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
        public ValueInput m_roleTypeMask { get; private set; }
        
        [DoNotSerialize]
        public ValueInput m_workerArea { get; private set; }
        
        /// <summary>
        /// 连续点单数量
        /// </summary>
        [DoNotSerialize]
        public ValueInput m_maxTakeOrderNum { get; private set; }

        [DoNotSerialize]
        public ControlInput m_input;

        [DoNotSerialize]
        public ControlOutput m_output;

        private List<Character> m_result = new List<Character>();
        private AotList m_resultObject = new AotList();

        protected override void Definition()
        {
            m_roleTypeMask = ValueInput<int>("roleTypeMask", 5);
            m_workerArea = ValueInput<int>("workerArea", 0);
            m_maxTakeOrderNum = ValueInput<int>("MaxTakeOrderNum", 0);
            value       = ValueOutput<AotList>("value", (flow)=> m_resultObject);

            m_input = ControlInput("Input", (flow) =>
            {
                int roleTypeMask = flow.GetValue<int>(m_roleTypeMask);
                int maxTakeOrderNum = flow.GetValue<int>(m_maxTakeOrderNum);
                int area = flow.GetValue<int>(m_workerArea);

                if (maxTakeOrderNum <= 0)
                {
                    CharacterModule.Instance.FindCharacters(m_result, (character) =>
                        BitOperator.Get(character.workerAread, area)           // 区域匹配
                        && character.isIdle                                                  // 状态匹配
                        && BitOperator.Get(roleTypeMask, character.roleType));          // (character.roleType == roleType1 || character.roleType == roleType2));
                }
                else
                {
                    CharacterModule.Instance.FindCharacters(m_result, (character) =>
                        BitOperator.Get(character.workerAread, area)           // 区域匹配
                        && character.isIdle                                                  // 状态匹配
                        && character.takeOrderNum <  maxTakeOrderNum                         // 连续接单数量
                        && BitOperator.Get(roleTypeMask, character.roleType)); 
                }

                m_resultObject.Clear();
                foreach (var item in m_result)
                    m_resultObject.Add(item);

                return m_output;
            });

            m_output = ControlOutput("Output");
        }
    }
}
