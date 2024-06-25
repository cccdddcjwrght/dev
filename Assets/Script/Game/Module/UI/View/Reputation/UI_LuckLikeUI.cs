/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Reputation
{
    public partial class UI_LuckLikeUI : GLabel
    {
        public Controller m_reward;
        public Controller m_auto;
        public GGraph m_closeBg;
        public GButton m_close;
        public GTextField m_name;
        public GGroup m_top_icon;
        public GList m_rewardList;
        public GGroup m_reward_group;
        public GButton m_startBtn;
        public GButton m_stopBtn;
        public GTextField m_count;
        public GList m_list1;
        public GList m_list2;
        public GGroup m_content;
        public UI_BigLuckShow m_BigLuckShow;
        public GGraph m___effect;
        public GGraph m_fly_effect1;
        public GGraph m_fly_effect2;
        public Transition m_t0;
        public Transition m_t1;
        public const string URL = "ui://vnok3a30wbw3f";

        public static UI_LuckLikeUI CreateInstance()
        {
            return (UI_LuckLikeUI)UIPackage.CreateObject("Reputation", "LuckLikeUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_reward = GetControllerAt(0);
            m_auto = GetControllerAt(1);
            m_closeBg = (GGraph)GetChildAt(0);
            m_close = (GButton)GetChildAt(1);
            m_name = (GTextField)GetChildAt(4);
            m_top_icon = (GGroup)GetChildAt(9);
            m_rewardList = (GList)GetChildAt(11);
            m_reward_group = (GGroup)GetChildAt(12);
            m_startBtn = (GButton)GetChildAt(17);
            m_stopBtn = (GButton)GetChildAt(19);
            m_count = (GTextField)GetChildAt(21);
            m_list1 = (GList)GetChildAt(23);
            m_list2 = (GList)GetChildAt(24);
            m_content = (GGroup)GetChildAt(30);
            m_BigLuckShow = (UI_BigLuckShow)GetChildAt(31);
            m___effect = (GGraph)GetChildAt(32);
            m_fly_effect1 = (GGraph)GetChildAt(33);
            m_fly_effect2 = (GGraph)GetChildAt(34);
            m_t0 = GetTransitionAt(0);
            m_t1 = GetTransitionAt(1);
        }
    }
}