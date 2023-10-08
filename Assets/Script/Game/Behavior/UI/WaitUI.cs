
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using Unity.Entities;
using SGame.UI;

namespace SGame.BT
{
    [TaskDescription("Wating UI open")]
    [TaskIcon("{SkinColor}LogIcon.png")]
    [TaskCategory("UI")]
    public class WaitUIOpen : Action
    {
        [BehaviorDesigner.Runtime.Tasks.Tooltip("要等待的UI")]
        public SharedVector2Int uiEntity;
        
        [BehaviorDesigner.Runtime.Tasks.Tooltip("超时等待时间")]
        public SharedFloat      waitTime;
        
        private EntityManager   m_entityManager;
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
            m_entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        }
        
        public override TaskStatus OnUpdate()
        {
            var e = entity;
            if (e == Entity.Null || m_entityManager.Exists(e) == false)
                return TaskStatus.Failure;

            
            if (waitTime != null && waitTime.Value > m_runTime)
            {
                m_runTime += Time.deltaTime;
                return TaskStatus.Running;
            }

            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            // Reset the properties back to their original values
            uiEntity = Vector2Int.zero;
            m_runTime = 0;
            waitTime = 1.0f;
        }
    }
}