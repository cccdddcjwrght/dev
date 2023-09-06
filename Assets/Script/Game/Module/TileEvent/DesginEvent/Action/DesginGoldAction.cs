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
            int travelState = (int)userProperty.GetNum((int)UserType.TRAVEL);
            int goldId = travelState == 0 ? (int)UserType.GOLD : (int)UserType.TRAVEL_GOLD;
            userProperty.AddNum(goldId, Value);
            long newValue = userProperty.GetNum(goldId);
            
            if (travelState == 0)
                // 添加普通金币
                EventManager.Instance.Trigger((int)GameEvent.PROPERTY_GOLD, Value, newValue, m_playerId);
            else
                // 添加出行金币
                EventManager.Instance.Trigger((int)GameEvent.PROPERTY_TRAVEL_GOLD, Value, newValue, m_playerId);
        }
    }
}