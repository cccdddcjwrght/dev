using System;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using log4net;
using Unity.Mathematics;

namespace SGame
{
    
    // 角色移动组件
    public class CharacterMover : IComponentData
    {
        [NonSerialized]
        public List<float3>             m_paths = new List<float3>(32);        // 移动路径

        // 已经移动距离
        public float                    m_movedDistance;

        // 每次移动间隔时间
        public float                    m_Intervaltime;

        // 每次移动间隔初始时间
        public float                    m_IntervaltimeReset;
        
        // 角色控制器, 用于实际移动, 角色初始化的时候会自动设置
        public CharacterController      m_controller;
        
        [NonSerialized]
        private static ILog             log                     = LogManager.GetLogger("xl.Character");
        

        private int                     m_currentIndex = 0;

        public int                      m_startTileId = 0;          // 开始到结束的ID

        public int                      currentIndex      { get { return m_currentIndex; } }

        // 当前的TileID
        public int                      currentTile         { get { return m_startTileId + m_currentIndex; } }

        // 是否结束移动了
        public bool                     isFinish           { get { return m_currentIndex >= m_paths.Count - 1; } }

        /// <summary>
        /// 重置时间计数
        /// </summary>
        public void ResetTimer()
        {
            m_Intervaltime = m_IntervaltimeReset;
        }

        public float3 LastPosition()
        {
            return m_paths[m_paths.Count - 1];
        }

        public void Clear()
        {
            m_movedDistance = 0;
            m_currentIndex = 0;
            m_paths.Clear();
        }

        public void Finish()
        {
            m_startTileId  += m_paths.Count - 1;
            m_movedDistance = 0;
            m_currentIndex  = 0;
            m_Intervaltime  = 0;
            m_IntervaltimeReset = 0;
            m_paths.Clear();
        }

        // 计算距离路径
        public float CalcPathDistance()
        {
            return GetDistance(m_paths.Count - 1);
        }

        /// <summary>
        /// 获得距离
        /// </summary>
        /// <param name="posIndex"></param>
        /// <returns></returns>
        public float GetDistance(int posIndex)
        {
            float d = 0;
            for (int i = 1; i <= posIndex; i++)
            {
                d += math.length(m_paths[i] - m_paths[i - 1]);
            }

            return d;
        }

        // 获得角色当前朝向
        /// <summary>
        /// 通过当前节点与下一个节点, 计算出角色的朝向
        /// </summary>
        /// <returns></returns>
        public quaternion GetRotation()
        {
            // 无效的
            if (m_paths.Count <= 1 || m_currentIndex >= m_paths.Count)
            {
                return quaternion.identity;
            }
            
            float3 pos1 = float3.zero;
            float3 pos2 = float3.zero;
            if (m_currentIndex == m_paths.Count - 1)
            {
                // 最后一个
                pos1 = m_paths[m_currentIndex - 1];
                pos2 = m_paths[m_currentIndex];

            }
            else
            {
                // 正常
                pos1 = m_paths[m_currentIndex];
                pos2 = m_paths[m_currentIndex + 1];
            }

            float3 dir = pos2 - pos1;
            dir.y = 0;
            quaternion ret = quaternion.LookRotation(dir, new float3(0, 1, 0));
            return ret;
        }
        
        /// <summary>
        /// 获得当前位置
        /// </summary>
        /// <returns></returns>
        public float3 GetPosition()
        {
            if (isFinish)
                return m_paths[m_paths.Count - 1];

            int currentIndex = m_currentIndex;
            float3 moveData = m_paths[currentIndex + 1] - m_paths[currentIndex];
            float3 dir      = math.normalize(moveData);
            float  len      = math.length(dir);
            if (len <= m_movedDistance)
            {
                // 已经超出移动范围了, 以节点数值未准
                return m_paths[currentIndex + 1];
            }
            
            float3 pos = m_paths[currentIndex] + dir * m_movedDistance;
            return pos;
        }

        /// <summary>
        /// 获得当前节点的移动进度, 注意不是整体进度, 是节点间的进度
        /// </summary>
        /// <returns></returns>
        public float GetMoveProgress()
        {
            if (isFinish)
                return 1.0f;

            float nDistance = nodeDistance;
            if (m_movedDistance >= nDistance)
                return 1.0f;

            return m_movedDistance / nDistance;
        }

        /// <summary>
        /// 获取节点开始位置 
        /// </summary>
        /// <returns></returns>
        public float3 GetStartPosition()
        {
            return m_paths[m_currentIndex];
        }

        public float3 GetNextPosition()
        {
            return m_paths[m_currentIndex + 1];
        }

        // 节点之间距离
        public float nodeDistance { get { if (isFinish) return 0; return math.length(m_paths[m_currentIndex + 1] - m_paths[m_currentIndex]); } }

        /// <summary>
        /// 移动一个距离
        /// </summary>
        /// <param name="delta"></param>
        public bool Movement(float delta)
        {
            if (isFinish)
                return false;
            
            m_movedDistance += delta;
            float ndistance = nodeDistance;
            if (ndistance <= m_movedDistance)
            {
                m_movedDistance -= ndistance;
                m_currentIndex += 1;
            }
            return true;
        }

        // 移动到特定位置
        /// <summary>
        /// 
        /// </summary>
        /// <param name="paths">paths移动的路径</param>
        /// <param name="startTileId">开始移动的ID</param>
        public void MoveTo(List<float3> paths, int startTileId, float intervalTime)
        {
            if (paths.Count < 2)
            {
                log.Error("Path Count Less 2!");
                return;
            }

            m_IntervaltimeReset = intervalTime;
            m_Intervaltime   = 0;
            m_movedDistance  = 0;
            m_currentIndex   = 0;
            m_startTileId    = startTileId;
            m_paths.Clear();
            m_paths.AddRange(paths);
            //m_distance = CalcPathDistance(); // 统计移动长度
        }
    }
    

}
