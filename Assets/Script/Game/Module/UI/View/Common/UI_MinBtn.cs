/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Common
{
    public partial class UI_MinBtn : GButton
    {
        public Controller m_bgColor;
        public Controller m_hasIcon;
        public Controller m_txtColor;
        public Controller m_iconImage;
        public Controller m_gray;
        public Controller m_limit;
        public Controller m_iconsize;
        public Controller m_txtSize;
        public GLoader m_bg;
        public GTextField m_iconTitle;
        public GGraph m___effect;
        public Transition m_t0;
        public const string URL = "ui://2w8thcm7en1x3lkz";

        public static UI_MinBtn CreateInstance()
        {
            return (UI_MinBtn)UIPackage.CreateObject("Common", "MinBtn");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_bgColor = GetControllerAt(1);
            m_hasIcon = GetControllerAt(2);
            m_txtColor = GetControllerAt(3);
            m_iconImage = GetControllerAt(4);
            m_gray = GetControllerAt(5);
            m_limit = GetControllerAt(6);
            m_iconsize = GetControllerAt(7);
            m_txtSize = GetControllerAt(8);
            m_bg = (GLoader)GetChildAt(0);
            m_iconTitle = (GTextField)GetChildAt(3);
            m___effect = (GGraph)GetChildAt(4);
            m_t0 = GetTransitionAt(0);
        }
    }
}