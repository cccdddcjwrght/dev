/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Setting
{
    public partial class UI_SignBtn : GButton
    {
        public Controller m_button;
        public Controller m_signSate;
        public GLoader m_bg;
        public GLoader m_icon;
        public GTextField m_title;
        public const string URL = "ui://dpgt0e2zn1bm1y";

        public static UI_SignBtn CreateInstance()
        {
            return (UI_SignBtn)UIPackage.CreateObject("Setting", "SignBtn");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_button = GetControllerAt(0);
            m_signSate = GetControllerAt(1);
            m_bg = (GLoader)GetChildAt(0);
            m_icon = (GLoader)GetChildAt(1);
            m_title = (GTextField)GetChildAt(2);
        }
    }
}