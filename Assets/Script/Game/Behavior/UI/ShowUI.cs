
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using Unity.Entities;
using SGame.UI;

namespace SGame.BT
{
    [TaskDescription("Open UI")]
    [TaskIcon("{SkinColor}LogIcon.png")]
    [TaskCategory("UI")]
    public class ShowUI : Action
    {
        [BehaviorDesigner.Runtime.Tasks.Tooltip("UI Name")]
        public SharedString uiName;

        [BehaviorDesigner.Runtime.Tasks.Tooltip("UI Param Name")]
        public SharedFloat  floatParam;
        
        [BehaviorDesigner.Runtime.Tasks.Tooltip("wait ui open")]
        public SharedFloat  waitTime;

        [BehaviorDesigner.Runtime.Tasks.Tooltip("UI Return Entity")]
        public SharedVector2Int uiEntity;

        [BehaviorDesigner.Runtime.Tasks.Tooltip("Need Wait UI")]
        public SharedBool     waitUI;

        private EntityManager m_entityManager;
        private bool          m_isOpenUI;

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
            m_isOpenUI      = false;
            uiEntity        = Vector2Int.zero;
        }

        void OpenUI()
        {
            m_isOpenUI = true;
            Entity e = UIRequest.Create(m_entityManager, UIUtils.GetUI(uiName.Value));
            uiEntity.Value = new Vector2Int(e.Index, e.Version);

            if (m_entityManager.HasComponent<UIParamFloat>(e) == false)
            {
                m_entityManager.AddComponentData(e, new UIParamFloat() { Value = floatParam.Value });
            }
            else
            {
                m_entityManager.SetComponentData(e, new UIParamFloat() { Value = floatParam.Value });
            }
        }


        public override TaskStatus OnUpdate()
        {
            var mgr = m_entityManager;
            if (waitUI != null && waitUI.Value)
            {
                if (m_isOpenUI == false)
                {
                    OpenUI();
                    return TaskStatus.Running;
                }
                else
                {
                    // UI 无效了
                    var e = entity;
                    if (e == Entity.Null || mgr.Exists(e))
                    {
                        return TaskStatus.Failure;;
                    }
                    if (mgr.HasComponent<DespawningEntity>(e))
                        return TaskStatus.Failure;

                    // UI 初始化完毕了
                    if (mgr.HasComponent<UIInitalized>(e))
                        return TaskStatus.Success;

                    if (waitTime != null && waitTime.Value > 0)
                    {
                        waitTime.Value -= Time.deltaTime;
                        if (waitTime.Value <= 0)
                        {
                            // 打开UI超时
                            waitTime.Value = 0;
                            return TaskStatus.Failure;
                        }
                    }

                    // UI 等待中
                    return TaskStatus.Running;
                }
            }
            else
            {
                if (!m_isOpenUI)
                    OpenUI();
                return entity != Entity.Null ? TaskStatus.Success : TaskStatus.Failure;
            }
        }

        public override void OnReset()
        {
            // Reset the properties back to their original values
            uiEntity   = Vector2Int.zero;
            m_isOpenUI = false;
        }
    }
}