
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using Unity.Entities;
using SGame.UI;

namespace SGame.BT
{
    [TaskDescription("Close UI")]
    [TaskIcon("{SkinColor}LogIcon.png")]
    [TaskCategory("UI")]
    public class CloseUI : Action
    {
        [BehaviorDesigner.Runtime.Tasks.Tooltip("UI Return Entity")]
        public SharedVector2Int uiEntity;


        private EntityManager m_entityManager;

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
            uiEntity        = Vector2Int.zero;
        }
        
        public override TaskStatus OnUpdate()
        {
            if (UIUtils.CloseUI(m_entityManager, entity))
                return TaskStatus.Success;

            return TaskStatus.Failure;
        }

        public override void OnReset()
        {
            // Reset the properties back to their original values
            uiEntity   = Vector2Int.zero;
        }
    }
}