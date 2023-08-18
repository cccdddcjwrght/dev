using SGame.UI;
using log4net;
using SGame.UI.Login;
using Unity.Entities;
using UnityEngine;

namespace SGame
{
    public class UILogin : IUIScript
    {
        private static ILog         log         = LogManager.GetLogger("xl.gameui");
        private FairyGUI.GTextField m_text;
        private UI_Login            m_view;
        
        public void OnInit(UIContext context)
        {
            m_view = context.content as UI_Login;
            m_view.m_btn_login.onClick.Add(OnClick);
            m_view.m_account.text = PlayerPrefs.GetString("user", "test");
        }

        // 按下按钮
        public void OnClick()
        {
            if (!string.IsNullOrEmpty(m_view.m_account.text))
            {
                PlayerPrefs.SetString("user", m_view.m_account.text);
                EventManager.Instance.Trigger((int)GameEvent.ENTER_LOGIN, m_view.m_account.text);
            }
        }

        public static IUIScript Create() { return new UILogin(); }
    }
}
