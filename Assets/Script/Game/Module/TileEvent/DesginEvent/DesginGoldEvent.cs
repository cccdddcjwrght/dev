using System.Collections;
using System.Collections.Generic;
using log4net;
using UnityEngine;


namespace SGame
{
    /// <summary>
    /// 地块事件执行逻辑
    /// </summary>
    public class DesginGoldEvent : IDesginEvent
    {
        private static ILog log = LogManager.GetLogger("DesginGold");
        
        // 添加金币
        public int Value;
        
        public override void Do()
        {
            log.Info("Execute Gold=" + Value.ToString());
        }
    }
}