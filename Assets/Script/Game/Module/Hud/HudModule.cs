using SGame.UI;
using Unity.Entities;

namespace SGame
{
    public class HudModule :  IModule
    {
        private GameWorld m_gameWorld;
        private Entity    m_hudUI;
        //private SystemCollection m_systems;

        private static HudModule s_instance = null;
        
        public static HudModule Instance { get { return s_instance; } }

        private UIWindow m_hudUIWindow;
        
        public bool IsReadly { get { return m_hudUIWindow != null; } }
        
        public UIWindow GetHUD()
        {
            return m_hudUIWindow;// EntityManager.GetComponentObject<UIWindow>(m_hudUI);
        }
        
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

        public void Shutdown()
        {
            //m_systems.Shutdown(World);
        }
    }
}