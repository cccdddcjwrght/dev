using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameConfigs;
using log4net;
using log4net.Core;
using Sirenix.Utilities;
using UnityEngine;
using Unity.Mathematics;

namespace SGame
{
    /// <summary>
    /// 排队功能
    /// </summary>
    public class TableQueueManager : Singleton<TableQueueManager>
    {
        private static ILog log = LogManager.GetLogger("tablequeue");
        private Dictionary<int, List<int>> m_tableQqueue = new Dictionary<int, List<int>>();
        private Dictionary<int, int>        m_character2Table = new Dictionary<int, int>(); // 记录角色ID与table之间的关系
        
        public void Clear()
        {
            m_tableQqueue.Clear();
            m_character2Table.Clear();
        }

        /// <summary>
        /// 离开队伍
        /// </summary>
        /// <param name="characterID"></param>
        /// <returns></returns>
        public bool Leave(int characterID)
        {
            var tableID = GetTable(characterID);
            var table = TableManager.Instance.Get(tableID);
            if (table == null)
            {
                log.Error("table id not found=" + tableID);
                return false;
            }

            if (!m_tableQqueue.TryGetValue(tableID, out List<int> listData))
            {
                log.Error("table queue not found=" + tableID);
                return false;
            }

            if (listData.Count <= 0 || listData[0] != characterID)
            {
                log.Error("Character Is Not Sit Chair=" + characterID);
                return false;
            }

            // 离开座位
            int chairIndex = table.FindFirstChair(CHAIR_TYPE.CUSTOMER);
            table.LeaveChair(characterID, chairIndex);
            listData.RemoveAt(0);
            m_character2Table.Remove(characterID);
            
            // 获取下一个角色
            var nextcharacterID = 0;
            if (listData.Count > 0)
            {
                nextcharacterID = listData[0];
                table.SitChair(nextcharacterID, chairIndex);
            }
            
            // 桌子更新
            EventManager.Instance.AsyncTrigger((int)GameEvent.TABLE_QUEUE_UPDATE, tableID, nextcharacterID);
            return true;
        }

        /// <summary>
        /// 获取桌子ID
        /// </summary>
        /// <param name="characterID"></param>
        /// <returns></returns>
        public int GetTable(int characterID)
        {
            if (m_character2Table.TryGetValue(characterID, out int tableID))
                return tableID;

            return -1;
        }

        /// <summary>
        /// 获取再队伍中的位置
        /// </summary>
        /// <param name="characterID"></param>
        /// <returns></returns>
        public int GetQueueIndex(int characterID)
        {
            int tableID = GetTable(characterID);
            if (tableID < 0)
                return -1;

            if (m_tableQqueue.TryGetValue(tableID, out List<int> listData))
            {
                var index = listData.IndexOf(characterID);
                if (index < 0)
                {
                    log.Error("not found id in table=" + characterID + " table=" + tableID);
                    return -1;
                }

                return index;
            }

            log.Error("not found id in table=" + characterID + " table=" + tableID);
            return -1;
        }

        /// <summary>
        /// 获取地图位置 
        /// </summary>
        /// <param name="characterID"></param>
        /// <returns></returns>
        public int2 GetMapPos(int characterID)
        {
            var tableID     = GetTable(characterID);
            var table          = TableManager.Instance.Get(tableID);
            var chairIndex  = table.FindFirstChair(CHAIR_TYPE.CUSTOMER);
            var chair          = table.GetChair(chairIndex);
            var pos        = chair.map_pos;

            int queueIndex     = GetQueueIndex(characterID);
            pos.y += queueIndex;
            return pos;
        }

        /// <summary>
        /// 排队
        /// </summary>
        /// <param name="characterID">角色ID</param>
        /// <returns></returns>
        public int AddQueue(int tableID, int characterID)
        {
            var table = TableManager.Instance.Get(tableID);
            if (table == null)
            {
                log.Error("table id not found=" + tableID);
                return -1;
            }

            if (!m_character2Table.TryAdd(characterID, tableID))
            {
                // 角色重复排队
                log.Error("character repeate add quue=" + characterID);
                return -1;
            }

            // 添加队列
            if (!m_tableQqueue.TryGetValue(tableID, out List<int> characterList))
            {
                characterList = new List<int>(10);
                m_tableQqueue.Add(tableID, characterList);
            }
            
            characterList.Add(characterID);
            if (characterList.Count == 1)
            {
                var chairIndex = table.FindFirstChair(CHAIR_TYPE.CUSTOMER);
                if (chairIndex < 0)
                {
                    log.Error("chair not found" + tableID);
                }
                else
                {
                    table.SitChair(characterID, chairIndex);
                }
            }
            return characterList.Count - 1;
        }
    }
}
