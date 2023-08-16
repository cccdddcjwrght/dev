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
        public List<float3>        m_paths = new List<float3>(32);        // 移动路径

        // 已经移动距离
        public float                m_movedDistance;

        // 每次移动间隔时间
        public float                m_Intervaltime;

        // 每次移动间隔初始时间
        public float                m_IntervaltimeReset;
        
        // 角色控制器, 用于实际移动, 角色初始化的时候会自动设置
        public CharacterController  m_controller;
        
        [NonSerialized]
        private static ILog         log                     = LogManager.GetLogger("xl.Character");
    
        // 移动总长度
        [NonSerialized]
        private float                 m_distance;

        public int                    m_currentIndex = 0;

        public int                    m_startTileId = 0;          // 开始到结束的ID

        public int                    currentTile         // 当前的TileID
        {
            get
            {
                return m_startTileId + m_currentIndex;
            }
        }

        public bool isFinish
        {
            get
            {
                return m_movedDistance >= m_distance;
            }
        }

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
            m_distance = 0;
            m_paths.Clear();
        }

        public void Finish()
        {
            m_startTileId  += m_paths.Count - 1;
            m_movedDistance = 0;
            m_currentIndex  = 0;
            m_distance      = 0;
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
        /// 获得位置起始点
        /// </summary>
        /// <param name="distance">检测的位置</param>
        /// <param name="upper_distance">上一个的位置</param>
        /// <returns>位置索引</returns>
        public int GetPositionIndex(float distance)
        {
            if (m_paths.Count < 2)
                return -1;

            if (distance >= m_distance)
                return m_paths.Count - 1;

            float d = 0;
            float3 startPos = m_paths[0];
            for (int i = 1; i < m_paths.Count; i++)
            {
                d += math.length(m_paths[i] - m_paths[i - 1]);

                if (d >= distance)
                {
                    return i - 1;
                }
            }

            return m_paths.Count - 1;
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

        public float3 GetDirection(int posIndex)
        {
            return m_paths[posIndex] - m_paths[posIndex + 1];
        }

        // 根据移动距离获取真实位置
        public bool GetPositionFromDistance(float distance, out float3 pos, out quaternion rot)
        {
            pos = float3.zero;
            rot = quaternion.identity;
            
            int posIndex = GetPositionIndex(distance);
            if (posIndex < 0)
                return false;

            float last_distance = GetDistance(posIndex);

            // 获得方向
            float3 dir = m_paths[posIndex + 1] - m_paths[posIndex];
            float distance2 = distance - last_distance;
            pos = m_paths[posIndex] + math.normalize(dir) * distance2;
            
            dir.y = 0;
            rot = quaternion.LookRotation(dir, new float3(0, 1, 0));
            return true;
        }


        void Awake()
        {
            m_paths = new List<float3>();
        }

        /// <summary>
        /// 移动一个距离
        /// </summary>
        /// <param name="delta"></param>
        public void Movement(float delta)
        {
            m_movedDistance += delta;
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
            m_distance = CalcPathDistance(); // 统计移动长度
        }

        public void Convert(
            Entity entity,
            EntityManager dstManager,
            GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentObject(entity, this);
        }
    }
    

}
