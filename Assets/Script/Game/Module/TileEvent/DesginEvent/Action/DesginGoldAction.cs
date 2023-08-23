using System.Collections;
using System.Collections.Generic;
using log4net;
using UnityEngine;


namespace SGame
{
    /// <summary>
    /// 银行事件
    /// </summary>
    public class DesginGoldAction : IDesginAction
    {
        private static ILog log = LogManager.GetLogger("DesginGold");
        
        // 添加金币
        public int Value;

        public int m_playerId;

        public DesginGoldAction(int add_gold, int PlayerId = 0)
        {
            Value       = add_gold;
            m_playerId  = PlayerId;
        }
        
        public override void Do()
        {
            var userProperty =  PropertyManager.Instance.GetUserGroup(m_playerId);

            // 执行添加金币事件
            userProperty.AddNum((int)UserType.GOLD, Value);
            long newValue = userProperty.GetNum((int)UserType.GOLD);
            
            EventManager.Instance.Trigger((int)GameEvent.PROPERTY_GOLD, Value, newValue, m_playerId);
        }
    }
}