/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Buff
{
    public partial class UI_ClickBtn : GButton
    {
        public Controller m_button;
        public Controller m_bgSize;
        public Controller m_txtSize;
        public Controller m_bgColor;
        public Controller m_hasIcon;
        public Controller m_txtColor;
        public Controller m_iconImage;
        public Controller m_gray;
        public Controller m_IconSize;
        public GLoader m_bg;
        public GTextField m_title;
        public GLoader m_icon;
        public GTextField m_shadow;
        public const string URL = "ui://g406runab32e8";

        public static UI_ClickBtn CreateInstance()
        {
            return (UI_ClickBtn)UIPackage.CreateObject("Buff", "ClickBtn");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_button = GetControllerAt(0);
            m_bgSize = GetControllerAt(1);
            m_txtSize = GetControllerAt(2);
            m_bgColor = GetControllerAt(3);
            m_hasIcon = GetControllerAt(4);
            m_txtColor = GetControllerAt(5);
            m_iconImage = GetControllerAt(6);
            m_gray = GetControllerAt(7);
            m_IconSize = GetControllerAt(8);
            m_bg = (GLoader)GetChildAt(0);
            m_title = (GTextField)GetChildAt(1);
            m_icon = (GLoader)GetChildAt(2);
            m_shadow = (GTextField)GetChildAt(3);
        }
    }
}