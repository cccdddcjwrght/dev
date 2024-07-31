/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Worktable
{
    public partial class UI_RecruitmentUI : GLabel
    {
        public Controller m_currency;
        public Controller m_type;
        public Controller m_roletype;
        public Controller m_recommand;
        public Controller m_selectctr;
        public GTextField m_desc;
        public GTextField m_areatips;
        public GTextField m_cost;
        public GButton m_click;
        public GButton m_close;
        public UI_RecBody m_roles;
        public const string URL = "ui://m8rpv7f9hx7up";

        public static UI_RecruitmentUI CreateInstance()
        {
            return (UI_RecruitmentUI)UIPackage.CreateObject("Worktable", "RecruitmentUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_currency = GetControllerAt(0);
            m_type = GetControllerAt(1);
            m_roletype = GetControllerAt(2);
            m_recommand = GetControllerAt(3);
            m_selectctr = GetControllerAt(4);
            m_desc = (GTextField)GetChildAt(5);
            m_areatips = (GTextField)GetChildAt(6);
            m_cost = (GTextField)GetChildAt(7);
            m_click = (GButton)GetChildAt(10);
            m_close = (GButton)GetChildAt(12);
            m_roles = (UI_RecBody)GetChildAt(14);
        }
    }
}