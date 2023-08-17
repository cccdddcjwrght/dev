using FairyGUI;
using log4net;
using SGame.UI;
using Unity.Entities;
using SGame.UI.Hud;
using UnityEngine;

namespace SGame
{
    public struct FloatTextData : IComponentData
    {
        // 对象池ID
        public PoolID Value;
    }
    
    [DisableAutoCreation]
    public partial class SpawnFloatTextSystem : SystemBase
    {
        private EntityArchetype          m_floatTextType;
        public  ObjectPool<UI_FloatText> m_floatComponents = new ObjectPool<UI_FloatText>();
        private Entity                   m_hud;
        private bool                     m_isReadly;
        private GComponent               m_hudContent;
        private EndInitializationEntityCommandBufferSystem      m_commandBuffer;
        private static ILog log = LogManager.GetLogger("xl.game.floatext");

        public ObjectPool<UI_FloatText> pool
        {
            get { return m_floatComponents; }
        }

        public void Initalize(Entity hud)
        {
            m_hud      = hud;
            m_isReadly = false;
        }

        protected override void OnCreate()
        {
            base.OnCreate();
            m_commandBuffer = World.GetOrCreateSystem<EndInitializationEntityCommandBufferSystem>();
            m_floatTextType = EntityManager.CreateArchetype(typeof(LiveTime), typeof(FloatTextData));
            m_floatComponents = new ObjectPool<UI_FloatText>(Alloce, Spawn, Despawn);
        }

        UI_FloatText Alloce()
        {
            UI_FloatText obj = UI_FloatText.CreateInstance();
            m_hudContent.AddChild(obj);
            obj.enabled = false;
            return obj;
        }

        void Spawn(UI_FloatText floatText)
        {
            floatText.enabled = true;
            floatText.alpha = 0.0f;
        }

        void Despawn(UI_FloatText floatText)
        {
            log.Info("On Free");
            floatText.alpha = 0;
            floatText.enabled = false;
        }
        
        bool isReadly()
        {
            return m_isReadly;
        }

        /// <summary>
        /// 检测状态
        /// </summary>
        void CheckState()
        {
            if (m_isReadly == true)
                return;

            if (EntityManager.HasComponent<UIInitalized>(m_hud))
            {
                m_hudContent = EntityManager.GetComponentObject<UIWindow>(m_hud).Value.contentPane;
                m_isReadly = m_isReadly = true;
            }
        }

        void SetupFloatText(FloatTextRequest request, UI_FloatText floatText)
        {
            floatText.m_title.color = request.color;
            floatText.m_title.text = request.text;
            floatText.m_title.textFormat.size = request.fontSize;
            floatText.xy = UIUtils.WorldPosToUI(m_hudContent, request.position);
            floatText.alpha = 1.0f;
        }

        protected override void OnUpdate()
        {
            CheckState();
            
            // 必须等UI
            if (isReadly() == false)
                return;
            
            var commandBuffer = m_commandBuffer.CreateCommandBuffer();
            Entities.ForEach((Entity e, FloatTextRequest request) =>
            {
                PoolID id              = m_floatComponents.Alloc();
                Entity entityFloatText = commandBuffer.CreateEntity(m_floatTextType);
                log.Info("create id=" + id.ToString() + " text=" + request.text);
                
                commandBuffer.SetComponent(entityFloatText, new LiveTime() {Value = request.duration});
                commandBuffer.SetComponent(entityFloatText, new FloatTextData()      {Value = id});
                if (m_floatComponents.TryGet(id, out UI_FloatText floatText))
                {
                    SetupFloatText(request, floatText);
                }
                
                // 删除创建请求
                commandBuffer.DestroyEntity(e);
            }).WithoutBurst().Run();
        }
    }
}