using SGame;
using Unity.VisualScripting;

namespace SGame.VS
{
    /// <summary>
    /// 获得空余座位数量
    /// </summary>
	
	[UnitTitle("GetEmptyChairCount")] //The Custom Scripting Event node to receive the Event. Add "On" to the node title as an Event naming convention.
    [UnitCategory("Game/Table")]
    public sealed class GetEmptyChairCount : Unit
    {
        /// <summary>
        /// The value of the variable.
        /// </summary>
        [DoNotSerialize]
        [PortLabelHidden]
        public ValueOutput m_count { get; private set; }
        
        [DoNotSerialize]
        public ValueInput m_tableType { get; private set; }
        
        [DoNotSerialize]
        public ValueInput m_chairType { get; private set; }

        
        protected override void Definition()
        {
            m_count      = ValueOutput("count", Get);
            m_tableType  = ValueInput<TABLE_TYPE>("tableType", TABLE_TYPE.CUSTOM);
            m_chairType  = ValueInput<CHAIR_TYPE>("chairType", CHAIR_TYPE.CUSTOMER);
        }
        
        private int Get(Flow flow)
        {
            var tableType = flow.GetValue<TABLE_TYPE>(this.m_tableType);
            var chariType =  flow.GetValue<CHAIR_TYPE>(this.m_chairType);
            return TableManager.Instance.GetEmptyChairCount(tableType, chariType);
        }
    }
}
