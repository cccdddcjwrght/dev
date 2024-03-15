/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Worktable
{
    public partial class UI_WorktablePanelUI : GLabel
    {
        public Controller m_type;
        public Controller m_pos;
        public GGraph m_right;
        public GGraph m_left;
        public GTextField m_level;
        public GList m_list;
        public GProgressBar m_progress;
        public GLabel m_reward;
        public GLabel m_time;
        public GLabel m_price;
        public GTextField m_unlock;
        public GButton m_click;
        public GGroup m_info;
        public GList m_tips;
        public GGroup m_view;
        public Transition m_t0;
        public const string URL = "ui://m8rpv7f9b32eb";

        public static UI_WorktablePanelUI CreateInstance()
        {
            return (UI_WorktablePanelUI)UIPackage.CreateObject("Worktable", "WorktablePanelUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_type = GetControllerAt(0);
            m_pos = GetControllerAt(1);
            m_right = (GGraph)GetChildAt(3);
            m_left = (GGraph)GetChildAt(4);
            m_level = (GTextField)GetChildAt(5);
            m_list = (GList)GetChildAt(7);
            m_progress = (GProgressBar)GetChildAt(8);
            m_reward = (GLabel)GetChildAt(9);
            m_time = (GLabel)GetChildAt(10);
            m_price = (GLabel)GetChildAt(11);
            m_unlock = (GTextField)GetChildAt(13);
            m_click = (GButton)GetChildAt(14);
            m_info = (GGroup)GetChildAt(15);
            m_tips = (GList)GetChildAt(16);
            m_view = (GGroup)GetChildAt(17);
            m_t0 = GetTransitionAt(0);
        }
    }
}