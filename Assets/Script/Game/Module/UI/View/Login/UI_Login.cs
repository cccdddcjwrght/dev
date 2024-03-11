/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Login
{
    public partial class UI_Login : GComponent
    {
        public Controller m_c1;
        public Controller m_sdkstate;
        public Controller m_loginstate;
        public Controller m_textSize;
        public GLoader m_Img_Bg;
        public GLabel m___logo;
        public GGroup m_Login;
        public GButton m_btn_login;
        public GGraph m_labelBg;
        public GTextInput m_account;
        public GGroup m_normal;
        public GTextField m_ver;
        public const string URL = "ui://yx22lbd8ojno1d";

        public static UI_Login CreateInstance()
        {
            return (UI_Login)UIPackage.CreateObject("Login", "Login");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_c1 = GetControllerAt(0);
            m_sdkstate = GetControllerAt(1);
            m_loginstate = GetControllerAt(2);
            m_textSize = GetControllerAt(3);
            m_Img_Bg = (GLoader)GetChildAt(0);
            m___logo = (GLabel)GetChildAt(1);
            m_Login = (GGroup)GetChildAt(2);
            m_btn_login = (GButton)GetChildAt(3);
            m_labelBg = (GGraph)GetChildAt(4);
            m_account = (GTextInput)GetChildAt(5);
            m_normal = (GGroup)GetChildAt(6);
            m_ver = (GTextField)GetChildAt(7);
        }
    }
}