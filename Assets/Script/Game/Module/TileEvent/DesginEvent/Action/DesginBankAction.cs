using System.Collections;
using System.Collections.Generic;
using log4net;
using UnityEngine;


namespace SGame
{
    /// <summary>
    /// 地块事件执行逻辑
    /// </summary>
    public class DesginBankAction : IDesginAction
    {
        private static ILog log = LogManager.GetLogger("xl.tileevent.DesginBankAction");
        
        // 银行ID
        int    m_buildingID;

        // 是存钱还是取钱
        bool   m_isSave; 
        
        // 添加金币
        int    m_gold;

        // 角色ID
        int    m_playerID;

        public DesginBankAction(int buildingID, bool isSave, int add_gold,  int playerId = 0)
        {
            m_buildingID  = buildingID;
            m_isSave      = isSave;
            m_gold        = add_gold;
            m_playerID    = playerId;
        }
        
        public override void Do()
        {
            if (BuildingModule.Instance.HasBuidlingData<BuildingBankData>(m_buildingID) == false)
            {
                log.Error("build not found= " + m_buildingID.ToString());
                return;
            }

            var bankData = BuildingModule.Instance.GetBuildingData<BuildingBankData>(m_buildingID);
            if (m_isSave)
            {
                // 存银行
                bankData.Value += m_gold;
                BuildingModule.Instance.SetBuildingData(m_buildingID, bankData);
            }
            else
            {
                // 取银行
                bankData.Value -= m_gold;
                
                // 执行添加金币事件
                var userProperty =  PropertyManager.Instance.GetUserGroup(m_playerID);
                userProperty.AddNum((int)UserType.GOLD, m_gold);
            }

        }
    }
}