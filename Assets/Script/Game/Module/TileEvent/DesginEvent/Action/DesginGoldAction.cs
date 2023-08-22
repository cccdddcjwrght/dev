using System.Collections;
using System.Collections.Generic;
using log4net;
using UnityEngine;


namespace SGame
{
    /// <summary>
    /// 银行事件
    /// </summary>
    public class DesginBankAction : IDesginAction
    {
        private static ILog log = LogManager.GetLogger("DesginGold");
        
        // 银行ID
        int    m_bankID;

        // 是存钱还是取钱
        bool   m_isSave; 
        
        // 添加金币
        int    m_gold;

        // 角色ID
        int    m_playerID;

        public DesginBankAction(int bankId, bool isSave, int add_gold,  int playerId = 0)
        {
            m_bankID    = bankId;
            m_isSave    = isSave;
            m_gold      = add_gold;
            m_playerID  = playerId;
        }
        
        public override void Do()
        {
           // var userProperty =  PropertyManager.Instance.GetUserGroup(PlayerId);
            // 执行添加金币事件
            // userProperty.AddNum((int)UserType.GOLD, Value);
            
            
        }
    }
}