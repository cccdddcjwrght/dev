using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using log4net;
using Unity.Mathematics;

namespace SGame
{
    // 角色移动组件
    public class CharacterMover : MonoBehaviour, IConvertGameObjectToEntity
    {
        // 移动路径
        public List<float3>        m_paths;

        // 已经移动距离
        public float                m_movedDistance;
        
        // 角色控制器, 用于实际移动, 角色初始化的时候会自动设置
        public CharacterController  m_controller;
        
        private static ILog         log                     = LogManager.GetLogger("xl.Character");

        public bool isFinish
        {
            get
            {
                return m_movedDistance >= CalcPathDistance();
            }
        }

        public void Clear()
        {
            //m_movedDistance = 0;
            m_paths.Clear();
        }

        // 计算距离路径
        public float CalcPathDistance()
        {
            if (m_paths.Count < 2)
                return 0;
                
            float d = 0;
            float3 startPos = m_paths[0];
            for (int i = 1; i < m_paths.Count; i++)
            {
                d += math.length(m_paths[i] - startPos);
                startPos = m_paths[i];
            }

            return d;
        }

        // 根据移动距离获取真实位置
        public float3 GetPositionFromDistance(float distance)
        {
            if (m_paths.Count < 2)
                return float3.zero;
            
            float d = 0;
            float3 startPos = m_paths[0];
            for (int i = 1; i < m_paths.Count; i++)
            {
                d += math.length(m_paths[i] - startPos);

                if (d >= distance)
                {
                    float small_distance = d - distance;

                    float3 dir = math.normalize(m_paths[i] - startPos);
                    float3 ret = m_paths[i] - dir * small_distance;
                    return ret;
                }
                
                startPos = m_paths[i];
            }

            return m_paths[m_paths.Count - 1];
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
        public void MoveTo(List<float3> paths)
        {
            if (paths.Count < 2)
            {
                log.Info("Path Count Less 2!");
                return;
            }
            
            m_movedDistance = 0;
            m_paths.Clear();
            m_paths.AddRange(paths);
        }

        // 移动到目标点
        public void MoveTo(float3 startPosition, float3 targetPosition)
        {
            List<float3> v = new List<float3>(2);
            v.Add(startPosition);
            v.Add(targetPosition);
            MoveTo(v);
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
