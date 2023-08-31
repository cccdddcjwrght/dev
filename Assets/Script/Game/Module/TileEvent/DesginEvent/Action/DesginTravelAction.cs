using System.Collections;
using System.Collections.Generic;
using log4net;
using UnityEngine;


namespace SGame
{
    /// <summary>
    /// 银行事件
    /// </summary>
    public class DesginTravelAction : IDesginAction
    {
        private static ILog log = LogManager.GetLogger("GameEvent.DesginTravelAction");
        

        public long m_travelPlayerId;

        public DesginTravelAction(long travelPlayerId)
        {
            m_travelPlayerId  = travelPlayerId;
        }
        
        public override void Do()
        {
            var userProperty =  PropertyManager.Instance.GetUserGroup(0);
            userProperty.SetNum((int)UserType.TRAVEL_PLAYERID, m_travelPlayerId);

            if (TileModule.Instance.currentMap == MapType.NORMAL)
            {
                EventManager.Instance.Trigger((int)GameEvent.TRAVEL_TRIGGER, true);
            }
            else
            {
                EventManager.Instance.Trigger((int)GameEvent.TRAVEL_TRIGGER, false);
            }
        }
    }
}