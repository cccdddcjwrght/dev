using System.Collections;
using System.Collections.Generic;
using log4net;
using UnityEngine;


namespace SGame
{
    /// <summary>
    /// 地块事件执行逻辑
    /// </summary>
    public class DesginGoldAction : IDesginAction
    {
        private static ILog log = LogManager.GetLogger("DesginGold");
        private ItemGroup  m_userProperty;
        
        // 添加金币
        public int Value;
        
        public override void Do()
        {
            // 执行添加金币事件
            m_userProperty.AddNum((int)UserType.GOLD, Value); 
            //UserSetting setting = DataCenter.Instance.GetUserSetting();
            //log.Info("Execute Gold=" + Value.ToString());
            //m_itemGroup.AddNum((int)UserType.GOLD, RandomSystem.Instance.NextInt(0, 10) * setting.doubleBonus);
        }
    }
}