using SGame.UI;
using log4net;
using Unity.Entities;
namespace SGame
{
    public class UILogin : IUIScript
    {
        private static ILog log = LogManager.GetLogger("xl.gameui");
        private UIContext          m_context;
        private FairyGUI.GTextField m_text;
        
        public void OnInit(UIContext context)
        {
            m_context = context;
            context.content.GetChild("btn_login").asButton.onClick.Add(OnClick);
            m_text = context.content.GetChild("account").asTextField;
        }

        public void OnClick()
        {
            EventManager.Instance.Trigger((int)GameEvent.ENTER_LOGIN);
        }

        public static IUIScript Create() { return new UILogin(); }
    }
}
