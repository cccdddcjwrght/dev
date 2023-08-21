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
        
        // 添加金币
        public int Value;

        public int PlayerId;

        public DesginGoldAction(int add_gold, int playerId = 0)
        {
            Value = add_gold;
            this.PlayerId = playerId;
        }
        
        public override void Do()
        {
           var userProperty =  PropertyManager.Instance.GetUserGroup(PlayerId);

            // 执行添加金币事件
            userProperty.AddNum((int)UserType.GOLD, Value);
        }
    }
}