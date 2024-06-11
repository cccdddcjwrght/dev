using System.Collections;
using System.Collections.Generic;
using GameTools;
using log4net;
using UnityEngine;
using Unity.Mathematics;

namespace SGame
{
    /// <summary>
    /// 顺序获得标签
    /// </summary>
    public class TagPositionGetter
    {
        private List<Vector2Int>    m_posiitons;
        private int                 m_index;
        private static ILog log = LogManager.GetLogger("game.character");

        public void Initalize(string tag)
        {
            m_posiitons = MapAgent.GetTagGrids(tag);
            if (m_posiitons == null || m_posiitons.Count == 0)
            {
                log.Error("not foudn tag=" + tag);
                return;
            }

            m_index = m_posiitons.Count / 2;
        }
        
        public static TagPositionGetter Create(string tag)
        {
            TagPositionGetter ret = new TagPositionGetter();
            ret.Initalize(tag);
            return ret;
        }

        public int2 GetNextPos()
        {
            m_index %= m_posiitons.Count;
            var ret = m_posiitons[m_index];
            m_index++;
            return new int2(ret.x, ret.y);
        }
    }
}