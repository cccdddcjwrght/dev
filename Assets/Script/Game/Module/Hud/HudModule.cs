using SGame.UI;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace SGame
{
    public enum TipType
    {
        BLUE    = 0, // 蓝色提示字
        YELLOW  = 1, // 黄色提示字
    }
    
    public class HudModule :  IModule
    {
        private GameWorld m_gameWorld;
        private Entity    m_hudUI;
        //private SystemCollection m_systems;

        private static HudModule s_instance = null;
        
        public static HudModule Instance { get { return s_instance; } }

        private UIWindow m_hudUIWindow;
        
        private static Entity m_sysTips = Entity.Null; // 系统文字提示
        
        public bool IsReadly { get { return m_hudUIWindow != null; } }
        
        public UIWindow GetHUD()
        {
            return m_hudUIWindow;
        }


        public HudModule(GameWorld world)
        {
            s_instance = this;
            m_gameWorld                 = world;

            // 创建UI
            m_hudUI = UIRequest.Create(EntityManager, UIUtils.GetUI("hud"));
            m_sysTips = Entity.Null;
        }

        EntityManager EntityManager
        {
            get
            {
                return m_gameWorld.GetEntityManager();
            }
        }

        World World
        {
            get
            {
                return m_gameWorld.GetECSWorld();
            }
        }

        public void Update()
        {
            if (m_hudUIWindow == null && m_hudUI != Entity.Null)
            {
                if (EntityManager.HasComponent<UIWindow>(m_hudUI) && EntityManager.HasComponent<UIInitalized>(m_hudUI))
                {
                    m_hudUIWindow = EntityManager.GetComponentObject<UIWindow>(m_hudUI);
                }
            }
        }

        /// <summary>
        /// 显示游戏内的浮动文字
        /// </summary>
        /// <param name="text"></param>
        /// <param name="pos"></param>
        public Entity ShowGameTips(string text, float3 pos, TipType tipType)
        {
            var entityManager   = UIModule.Instance.GetEntityManager();
            var uiEntity        = UIRequest.Create(entityManager, UIUtils.GetUI("GameTip"));
            entityManager.AddComponentData(uiEntity, new GameTipParam() { type = tipType, text = text });
            entityManager.AddComponentData(uiEntity, new Translation() { Value = pos });
            return uiEntity;
        }
        
        /// <summary>
        /// 系统的文字提示
        /// </summary>
        /// <param name="text"></param>
        public void SystemTips(object text)
        {
            //UIUtils.ShowHUD()
            var entityManager = UIModule.Instance.GetEntityManager();
            if (!UIModule.Instance.CheckOpened(m_sysTips))
            {
                m_sysTips = UIRequest.Create(entityManager, UIUtils.GetUI("SystemTip"));
                entityManager.AddComponentData(m_sysTips, new UIParam() { Value = text });
            }
            else
            {
                UIUtils.TriggerUIEvent(m_sysTips, "UpdateTip", text);
            }
        }

        public void ShowGoldTips(string text)
        {
            
        }

        public void Shutdown()
        {
            //m_systems.Shutdown(World);
        }
    }
}