using System.Collections;
using System.Collections.Generic;
using log4net;
using Unity.Entities;
using Unity.Mathematics;
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
        int m_buildingID;

        // 给银行添加金币, 负数就是取钱
        int m_gold;

        // 角色ID
        int m_playerID;

        public DesginBankAction(int buildingID, int add_gold, int playerId = 0)
        {
            m_buildingID = buildingID;
            m_gold = add_gold;
            m_playerID = playerId;
        }

        public override void Do()
        {
            if (BuildingModule.Instance.HasBuidlingData<BuildingBankData>(m_buildingID) == false)
            {
                log.Error("build not found= " + m_buildingID.ToString());
                return;
            }

            // 限制最小金币
            int buildEventId = Utils.GetBuildingEventId(m_buildingID, 1);
            if (!ConfigSystem.Instance.TryGet(buildEventId, out GameConfigs.Build_BankRowData buildEventConfig))
            {
                log.Error("bank build event id not found=" + buildEventId);
                return;
            }
            long minGold = (long)buildEventConfig.BasicRewardsCoin;
            var bankData = BuildingModule.Instance.GetBuildingData<BuildingBankData>(m_buildingID);
            bankData.Value += m_gold;
            bankData.Value = math.max(minGold, bankData.Value);

            if (m_gold < 0)
            {
                // 取银行
                // 执行添加金币事件
                var userProperty = PropertyManager.Instance.GetUserGroup(m_playerID);
                userProperty.AddNum((int)UserType.GOLD, -m_gold);
            }

            // 银行添加金币
            BuildingModule.Instance.SetBuildingData(m_buildingID, bankData);
            EventManager.Instance.Trigger((int)GameEvent.PROPERTY_BANK, m_gold, bankData.Value , m_buildingID, m_playerID);
        }
    }
}