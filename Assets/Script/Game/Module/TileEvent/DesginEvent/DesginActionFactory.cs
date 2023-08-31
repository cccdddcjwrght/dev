using System.Collections;
using System.Collections.Generic;
using GameConfigs;
using log4net;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

namespace SGame
{
    public class DesginActionFactory
    {
        private static ILog log = LogManager.GetLogger("xl.gameevent");
        

        public TileModule m_tileModule;

        public DesginActionFactory(TileModule tileModule)
        {
            m_tileModule = tileModule;
        }
        
        /// <summary>
        /// 创建金币事件
        /// </summary>
        /// <param name="addGold"></param>
        /// <returns></returns>
        public IDesginAction CreateGold(int addGold)
        {
            return new DesginGoldAction(addGold);
        }

        /// <summary>
        /// 根据节点创建事件
        /// </summary>
        /// <param name="e"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public IDesginAction Create(RoundEvent e, int pos)
        {
            int buildngID = m_tileModule.GetBuildingIDByPos(pos, m_tileModule.currentMap);
            if (buildngID <= 0)
            {
                log.Warn("build id not found pos=" + pos);
                return null;
            }
            
            switch (e.eventType)
            {
                case (int)Cs.EventType.Gold:
                    return CreateGold(e.gold);
                
                case (int)Cs.EventType.Bank:
                    return new DesginBankAction(buildngID, -e.gold);

                default:
                    log.Error("Error Not Support Event=" + ((Cs.EventType)e.eventType).ToString());
                    break;
            }

            return null;
        }

        /// <summary>
        /// 创建经过的事件
        /// </summary>
        /// <param name="e"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public IDesginAction CreatePass(int eventType, int tileId, int buildingID)
        {
            switch (eventType)
            {
                case (int)Cs.EventType.Gold:
                    return null;

                case (int)Cs.EventType.Bank:
                        // 添加路过添加银行事件
                        return new DesginBankPass(buildingID, 0);
                
                default:
                    log.Warn("CreatePass Error Not Support Event=" + ((Cs.EventType)eventType).ToString());
                    break;
            }
                
            return null;
        }
    }
}