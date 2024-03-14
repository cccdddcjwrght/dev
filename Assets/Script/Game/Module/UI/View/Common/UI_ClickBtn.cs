/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Common
{
    public partial class UI_ClickBtn : GButton
    {
        public Controller m_bgSize;
        public Controller m_txtSize;
        public Controller m_bgColor;
        public Controller m_hasIcon;
        public Controller m_txtColor;
        public Controller m_iconImage;
        public Controller m_gray;
        public Controller m_limit;
        public Controller m_iconsize;
        public GLoader m_bg;
        public GTextField m_iconTitle;
        public GGraph m___effect;
        public const string URL = "ui://2w8thcm7k0s63lb1";

        public static UI_ClickBtn CreateInstance()
        {
            return (UI_ClickBtn)UIPackage.CreateObject("Common", "ClickBtn");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_bgSize = GetControllerAt(1);
            m_txtSize = GetControllerAt(2);
            m_bgColor = GetControllerAt(3);
            m_hasIcon = GetControllerAt(4);
            m_txtColor = GetControllerAt(5);
            m_iconImage = GetControllerAt(6);
            m_gray = GetControllerAt(7);
            m_limit = GetControllerAt(8);
            m_iconsize = GetControllerAt(9);
            m_bg = (GLoader)GetChildAt(0);
            m_iconTitle = (GTextField)GetChildAt(3);
            m___effect = (GGraph)GetChildAt(4);
        }
    }
}