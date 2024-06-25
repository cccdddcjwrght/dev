/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Worktable
{
    public partial class UI_LockPanelUI : GLabel
    {
        public Controller m_btnty;
        public Controller m_type;
        public GTextField m_unlock;
        public GButton m_click;
        public GGroup m_info;
        public GTextField m_tips;
        public GGroup m_view;
        public Transition m_t0;
        public const string URL = "ui://m8rpv7f9k7tei";

        public static UI_LockPanelUI CreateInstance()
        {
            return (UI_LockPanelUI)UIPackage.CreateObject("Worktable", "LockPanelUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_btnty = GetControllerAt(0);
            m_type = GetControllerAt(1);
            m_unlock = (GTextField)GetChildAt(1);
            m_click = (GButton)GetChildAt(3);
            m_info = (GGroup)GetChildAt(4);
            m_tips = (GTextField)GetChildAt(5);
            m_view = (GGroup)GetChildAt(8);
            m_t0 = GetTransitionAt(0);
        }
    }
}