using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FlatBuffers;
using log4net;
using SGame;
using UnityEngine;

/// <summary>
/// 标记需要等待服务的座位
/// </summary>
public class WaitForServiceChair : Singleton<WaitForServiceChair>
{
    private static ILog log = LogManager.GetLogger("game.table");
    private Queue<ChairData> m_chairQuee = new Queue<ChairData>();
    
    /// <summary>
    /// 添加需要服务的座位
    /// </summary>
    /// <param name="chair"></param>
    public void Enqueue(ChairData chair)
    {
        var table = TableManager.Instance.Get(chair.tableID);
        if (table == null)
        {
            log.Error("table not found=" + chair.tableID);
            return;
        }
        
        chair = table.GetChair(chair.chairIndex);
        if (chair.playerID <= 0)
        {
            log.Error("player is zero=" + chair.tableID + " chairIndex=" + chair.chairIndex);
            return;
        }

        // 判断是否包含
        if (IsContains(chair.playerID))
        {
            log.Error("player is already add=" + chair.playerID);
            return;
        }
        
        m_chairQuee.Enqueue(chair);
    }

    bool IsContains(int playerID)
    {
        foreach (var item in m_chairQuee)
        {
            if (item.playerID == playerID)
                return true;
        }

        return false;
    }

    /// <summary>
    /// 获取需要服务的座位
    /// </summary>
    /// <param name="chair"></param>
    /// <returns></returns>
    public bool Dequeue(out ChairData chair)
    {
        chair = ChairData.Null;
        if (m_chairQuee.Count == 0)
            return false;

        chair = m_chairQuee.Dequeue();
        return true;
    }

    /// <summary>
    /// 判断当前是否有座位
    /// </summary>
    /// <returns></returns>
    public bool HasChair()
    {
        return m_chairQuee.Count > 0;
    }
}
