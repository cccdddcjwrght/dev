using System.Collections;
using System.Collections.Generic;
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
            
        private static ConfigValueFloat PET_START_ANGLE     = new ConfigValueFloat("pet_start_angle", 20);
        private static ConfigValueFloat PET_START_DISTANCE  = new ConfigValueFloat("pet_start_distance", 2);
        private Animator    m_animator;
        private static int WALK_NAME = 0;

        void Start()
        {
            if (WALK_NAME == 0)
                WALK_NAME = Animator.StringToHash("walk");
            m_animator = GetComponent<Animator>();
        }

        public void Initalzie(Transform follow, float radius, float speed, float scale)
        {
            m_followTarget  = follow;
            m_radius        = radius;
            m_transform     = transform;
            m_speed         =  speed;

            var rot2 = Quaternion.Euler(0, PET_START_ANGLE.Value, 0); // 绕Y轴旋转20度
            var dir = rot2 * m_followTarget.rotation * Vector3.forward;
            dir.y = 0;
            var offset = -dir.normalized * PET_START_DISTANCE.Value;

            m_transform.position = follow.position + offset;
            m_transform.rotation = Quaternion.identity;
            m_transform.localScale = new Vector3(scale, scale, scale);
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
            diff.y = 0;
            float   diffLen = diff.magnitude;
            if (diffLen <= (m_radius + 0.01f))
            {
                m_animator.SetBool(WALK_NAME, false);
                return;
            }

            // 圈外跟随
            m_animator.SetBool(WALK_NAME, true);
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
