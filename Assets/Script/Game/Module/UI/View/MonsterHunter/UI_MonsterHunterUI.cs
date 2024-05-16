/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.MonsterHunter
{
    public partial class UI_MonsterHunterUI : GLabel
    {
        public Controller m_play;
        public Controller m_auto;
        public Controller m_type;
        public GLoader m_bg;
        public GGraph m_monster;
        public GGraph m___effect;
        public GLabel m_body;
        public UI_MonsterHp m_hp;
        public GButton m_showreward;
        public GList m_list;
        public UI_HunterWheel m_panel;
        public UI_HunterRewardTips m_reward;
        public UI_HunterRewardList m_rewardsPreview;
        public const string URL = "ui://my7wqjw6twfo4";

        public static UI_MonsterHunterUI CreateInstance()
        {
            return (UI_MonsterHunterUI)UIPackage.CreateObject("MonsterHunter", "MonsterHunterUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_play = GetControllerAt(0);
            m_auto = GetControllerAt(1);
            m_type = GetControllerAt(2);
            m_bg = (GLoader)GetChildAt(0);
            m_monster = (GGraph)GetChildAt(1);
            m___effect = (GGraph)GetChildAt(2);
            m_body = (GLabel)GetChildAt(3);
            m_hp = (UI_MonsterHp)GetChildAt(4);
            m_showreward = (GButton)GetChildAt(5);
            m_list = (GList)GetChildAt(8);
            m_panel = (UI_HunterWheel)GetChildAt(9);
            m_reward = (UI_HunterRewardTips)GetChildAt(10);
            m_rewardsPreview = (UI_HunterRewardList)GetChildAt(11);
        }
    }
}