using SGame.UI;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using FairyGUI;

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
        
        public bool IsReadly { get { return m_hudUIWindow != null; } }

        public const int SYSTEM_TIP_ID = 28; // 系统提示UI
        
        public UIWindow GetHUD()
        {
            return m_hudUIWindow;
        }

        public GComponent GetHUDRoot() => m_hudUIWindow.BaseValue.content;


        public HudModule(GameWorld world)
        {
            s_instance = this;
            m_gameWorld                 = world;

            // 创建UI
            m_hudUI = UIRequest.Create(EntityManager, UIUtils.GetUI("hud"));
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
            entityManager.AddComponentData(uiEntity, LocalTransform.FromPosition(pos));
            return uiEntity;
        }
        
        /// <summary>
        /// 系统的文字提示
        /// </summary>
        /// <param name="text"></param>
        public void SystemTips(object text)
        {
            var entityManager = UIModule.Instance.GetEntityManager();
            var sysTips = UIModule.Instance.ShowSingleton(SYSTEM_TIP_ID);
            var state = UIModule.Instance.GetUIState(sysTips);
            if (state == UIModule.UI_STATE.REQUEST)
            {
                entityManager.AddComponentData(sysTips, new UIParam() { Value = text });
            }
            else
            {
                UIUtils.TriggerUIEvent(sysTips, "UpdateTip", text);
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