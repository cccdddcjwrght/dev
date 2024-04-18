using GameConfigs;
using log4net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame
{
    public enum AdType 
    {
        Level
    }

    public class AdModule : Singleton<AdModule>
    {
        private static ILog log = LogManager.GetLogger("game.ad");

        public readonly float   AD_MACHINE_RATION   = GlobalDesginConfig.GetFloat("ad_machine_ratio");
        public readonly int     AD_MACHINE_UP       = GlobalDesginConfig.GetInt("ad_machine_up");
        public readonly int     AD_MACHINE_NUM      = GlobalDesginConfig.GetInt("ad_machine_num");
        public readonly int     AD_BOOST_RATIO      = GlobalDesginConfig.GetInt("ad_boost_ratio");

        //public readonly int     AD_BOOST_TIME       = GlobalDesginConfig.GetInt("ad_boost_time");

        public const int AD_BUFF_ID = 7;

        private EventHandleContainer m_handles = new EventHandleContainer();

        public void Initalize() 
        {
            m_handles += EventManager.Instance.Reg<int>((int)GameEvent.ENTER_ROOM,(s)=> { AddBuff(); });
        }

        public void AddBuff() 
        {
            var time = DataCenter.AdUtil.GetSustainTime("Ad_Buff");
            if(time > 0) 
                EventManager.Instance.Trigger((int)GameEvent.BUFF_TRIGGER, new BuffData(AD_BUFF_ID, AD_BOOST_RATIO, 0, time){ from = (int)EnumFrom.Ad });
        }

     
    }
}
