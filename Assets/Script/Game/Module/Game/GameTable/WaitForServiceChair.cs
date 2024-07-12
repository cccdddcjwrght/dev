using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FlatBuffers;
using log4net;
using SGame;
using Unity.Entities;
using UnityEngine;

/// <summary>
/// 标记需要等待服务的座位
/// </summary>
public class WaitForServiceChair : Singleton<WaitForServiceChair>
{
    private static ILog log = LogManager.GetLogger("game.table");
    private Queue<ChairData> m_chairQuee = new Queue<ChairData>();

    public void Clear()
    {
        m_chairQuee.Clear();
    }
    
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
        if (chair.IsEmpty)
        {
            log.Error("player is zero=" + chair.tableID + " chairIndex=" + chair.chairIndex);
            return;
        }

        // 判断是否包含
        if (IsContains(chair))
        {
            log.Error("player is already add=" + chair.playerID);
            return;
        }
        
        m_chairQuee.Enqueue(chair);
    }

    bool IsContains(ChairData chair)
    {
        foreach (var item in m_chairQuee)
        {
            if (chair.playerID != 0 && item.playerID == chair.playerID)
                return true;

            if (chair.playerEntity != Entity.Null && item.playerEntity == chair.playerEntity)
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
    /// 获取第一个
    /// </summary>
    /// <param name="chair"></param>
    /// <returns></returns>
    public bool Peek(out ChairData chair)
    {
        chair = ChairData.Null;
        if (m_chairQuee.Count == 0)
            return false;

        chair = m_chairQuee.Peek();
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
