
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using Unity.Entities;
using SGame.UI;

namespace SGame.BT
{
    [TaskDescription("Wating UI open")]
    [TaskIcon("{SkinColor}WaitIcon.png")]
    [TaskCategory("UI")]
    public class WaitGameEvent : Action
    {
        [BehaviorDesigner.Runtime.Tasks.Tooltip("要等待的UI")]
        public SharedVector2Int uiEntity;
        
        [BehaviorDesigner.Runtime.Tasks.Tooltip("超时等待时间")]
        public SharedFloat      waitTime;
        
        [BehaviorDesigner.Runtime.Tasks.Tooltip("是否等待是是打开")]
        public SharedBool       isOpen;
        
        private EntityManager   m_mgr;
        private float           m_runTime = 0;
        
        private Entity entity
        {
            get
            {
                return uiEntity != null ? new Entity() { Index = uiEntity.Value.x, Version = uiEntity.Value.y } : Entity.Null;
            }
        }
        
        public override void OnStart()
        {
            m_mgr = World.DefaultGameObjectInjectionWorld.EntityManager;
        }
        
        public override TaskStatus OnUpdate()
        {
            var e = entity;
            if (e == Entity.Null)
                return TaskStatus.Failure;
            
            if (isOpen != null && isOpen.Value)
            {
                // 等待开启
                if (m_mgr.Exists(e) == false || m_mgr.HasComponent<DespawningEntity>(e))
                    return TaskStatus.Failure;

                // UI 开启了
                if (m_mgr.HasComponent<UIInitalized>(e))
                    return TaskStatus.Success;
            }
            else
            {
                // 等待结束
                if (m_mgr.Exists(e) == false || m_mgr.HasComponent<DespawningEntity>(e))
                    return TaskStatus.Success;
            }
            
            // 等待超时
            if (waitTime != null && waitTime.Value > m_runTime)
            {
                m_runTime += Time.deltaTime;
                return TaskStatus.Running;
            }
            else
            {
                return TaskStatus.Failure;
            }
        }

        public override void OnReset()
        {
            // Reset the properties back to their original values
            uiEntity = Vector2Int.zero;
            m_runTime = 0;
            waitTime = 1.0f;
            isOpen = true;
        }
    }
}