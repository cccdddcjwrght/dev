using System.Collections;
using System.Collections.Generic;
using OfficeOpenXml.Table.PivotTable;
using Unity.Mathematics;
using UnityEngine;

namespace SGame
{
    /// <summary>
    /// 宠物逻辑, 跟随目标
    /// </summary>
    public class PetLogic : MonoBehaviour
    {
        private Transform   m_followTarget;
        private float       m_radius;
        private Transform   m_transform;
        private float       m_speed = 1.0f;
        
        public void Initalzie(Transform follow, float radius, float speed)
        {
            m_followTarget  = follow;
            m_radius        = radius;
            m_transform     = transform;
            m_speed         =  speed;

            m_transform.position = follow.position;
            m_transform.rotation = Quaternion.identity;
        }

        // Update is called once per frame
        void Update()
        {
            // 目标已经销毁, 自动删除
            if (m_followTarget == null)
            {
                Destroy(gameObject);
                return;
            }

            // 圈内不移动 
            var currPos = m_transform.position;
            var targetPos = m_followTarget.position;
            Vector3 diff = m_followTarget.position - currPos;
            float   diffLen = diff.magnitude;
            if (diffLen <= m_radius)
                return;

            // 圈外跟随
            var target = Utils.GetCircleHitPoint(currPos, targetPos, m_radius);
            float moveLen = Time.deltaTime * m_speed;
            float t = moveLen / diffLen;
            var pos = Vector3.Lerp(currPos, target, t);
            pos.y = 0;
            
            // 设置位置
            m_transform.position = pos;

            // 设置旋转
            diff.y = 0;
            m_transform.rotation = Quaternion.LookRotation(diff, Vector3.up);
        }
    }
}
