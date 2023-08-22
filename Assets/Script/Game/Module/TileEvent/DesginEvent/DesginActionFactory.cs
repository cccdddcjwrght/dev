using System.Collections;
using System.Collections.Generic;
using log4net;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

namespace SGame
{
    public class DesginActionFactory
    {
        private static ILog log = LogManager.GetLogger("xl.gameevent");
        /// <summary>
        /// 创建金币事件
        /// </summary>
        /// <param name="addGold"></param>
        /// <returns></returns>
        public IDesginAction CreateGold(int addGold)
        {
            return new DesginGoldAction(addGold);
        }

        public IDesginAction Create(RoundEvent e)
        {
            switch (e.eventType)
            {
                case (int)Cs.EventType.Gold:
                    return CreateGold(e.gold);
                
                default:
                    log.Warn("Error Not Support Event=" + ((Cs.EventType)e.eventType).ToString());
                    break;
            }

            return null;
        }
    }
}