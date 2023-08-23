using System.Collections;
using System.Collections.Generic;
using log4net;
using Unity.Entities;
using UnityEngine;


namespace SGame
{
    /// <summary>
    /// 地块事件执行逻辑
    /// </summary>
    public class DesginBankPass : IDesginAction
    {
        private static ILog log = LogManager.GetLogger("xl.tileevent.DesginBankPass");

        // 银行ID
        int m_buildingID;
        
        // 角色ID
        int m_playerID;

        public DesginBankPass(int buildingID, int playerId = 0)
        {
            m_buildingID = buildingID;
            m_playerID = playerId;
        }

        public override void Do()
        {
            if (BuildingModule.Instance.HasBuidlingData<BuildingBankData>(m_buildingID) == false)
            {
                log.Error("build not found= " + m_buildingID.ToString());
                return;
            }

            var bankData = BuildingModule.Instance.GetBuildingData<BuildingBankData>(m_buildingID);
            var buildData = BuildingModule.Instance.GetBuildingData<BuildingData>(m_buildingID);

            int buildEventId = Utils.GetBuildingEventId(m_buildingID, buildData.level);
            if (buildEventId <= 0)
            {
                log.Error("buildevent id not found=" + m_buildingID);
                return;
            }

            if (!ConfigSystem.Instance.TryGet(buildEventId, out GameConfigs.Build_BankRowData bankConfig))
            {
                log.Error("buildevent id2 not found=" + m_buildingID);
                return;
            }

            // 路过加金币
            bankData.Value += bankConfig.PassByAddCoin;

            // 银行添加金币
            BuildingModule.Instance.SetBuildingData(m_buildingID, bankData);
            EventManager.Instance.Trigger((int)GameEvent.PROPERTY_BANK, bankConfig.PassByAddCoin, bankData.Value , m_buildingID, m_playerID);
        }
    }
}