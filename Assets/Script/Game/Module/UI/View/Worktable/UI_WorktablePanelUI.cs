/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Worktable
{
    public partial class UI_WorktablePanelUI : GLabel
    {
        public Controller m_type;
        public Controller m_pos;
        public Controller m_isAd;
        public Controller m_btnty;
        public Controller m_maxlv;
        public Controller m_roleType;
        public GGraph m_right;
        public GGraph m_left;
        public GTextField m_level;
        public GList m_list;
        public GProgressBar m_progress;
        public GList m_rewardlist;
        public GLabel m_time;
        public GLabel m_price;
        public GTextField m_next;
        public GTextField m_now;
        public GTextField m_now1;
        public GTextField m_unlock;
        public GLoader m_clickBtn;
        public GButton m_click;
        public GGroup m_info;
        public GList m_tips;
        public GButton m_adBtn;
        public GLoader m_adArea;
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
            m_isAd = GetControllerAt(2);
            m_btnty = GetControllerAt(3);
            m_maxlv = GetControllerAt(4);
            m_roleType = GetControllerAt(5);
            m_right = (GGraph)GetChildAt(0);
            m_left = (GGraph)GetChildAt(1);
            m_level = (GTextField)GetChildAt(6);
            m_list = (GList)GetChildAt(8);
            m_progress = (GProgressBar)GetChildAt(9);
            m_rewardlist = (GList)GetChildAt(10);
            m_time = (GLabel)GetChildAt(11);
            m_price = (GLabel)GetChildAt(12);
            m_next = (GTextField)GetChildAt(16);
            m_now = (GTextField)GetChildAt(19);
            m_now1 = (GTextField)GetChildAt(22);
            m_unlock = (GTextField)GetChildAt(25);
            m_clickBtn = (GLoader)GetChildAt(26);
            m_click = (GButton)GetChildAt(28);
            m_info = (GGroup)GetChildAt(30);
            m_tips = (GList)GetChildAt(31);
            m_adBtn = (GButton)GetChildAt(32);
            m_adArea = (GLoader)GetChildAt(33);
            m_view = (GGroup)GetChildAt(34);
            m_t0 = GetTransitionAt(0);
        }
    }
}