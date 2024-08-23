/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Explore
{
    public partial class UI_ExploreUI : GLabel
    {
        public Controller m_exploreState;
        public Controller m_exploreAuto;
        public Controller m_eqinfostate;
        public GLoader m_map;
        public GLabel m_topbar;
        public GButton m_close;
        public UI_ExploreProgress m_progress;
        public GTextField m_round;
        public GGroup m_roundGroup;
        public UI_FightEquip m_eq11;
        public UI_FightEquip m_eq12;
        public UI_FightEquip m_eq13;
        public UI_FightEquip m_eq20;
        public GButton m_find;
        public UI_ToolBtn m_tool;
        public GButton m_auto;
        public GButton m_help;
        public UI_FightAttr m_hp;
        public UI_FightAttr m_atk;
        public GTextField m_power;
        public UI_FightEquipInfo m_eqinfo;
        public GGraph m_holder;
        public GGraph m_mholder;
        public GLoader m_monster;
        public GGraph m_battlemonster;
        public GTextField m_exptips;
        public GButton m_fightBtn;
        public GProgressBar m_fightHp1;
        public GProgressBar m_fightHp2;
        public Transition m_kill;
        public Transition m_expanim;
        public Transition m_showeqinfo;
        public const string URL = "ui://ow12is1hpm5b14";

        public static UI_ExploreUI CreateInstance()
        {
            return (UI_ExploreUI)UIPackage.CreateObject("Explore", "ExploreUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_exploreState = GetControllerAt(0);
            m_exploreAuto = GetControllerAt(1);
            m_eqinfostate = GetControllerAt(2);
            m_map = (GLoader)GetChildAt(0);
            m_topbar = (GLabel)GetChildAt(1);
            m_close = (GButton)GetChildAt(3);
            m_progress = (UI_ExploreProgress)GetChildAt(4);
            m_round = (GTextField)GetChildAt(6);
            m_roundGroup = (GGroup)GetChildAt(7);
            m_eq11 = (UI_FightEquip)GetChildAt(12);
            m_eq12 = (UI_FightEquip)GetChildAt(13);
            m_eq13 = (UI_FightEquip)GetChildAt(14);
            m_eq20 = (UI_FightEquip)GetChildAt(15);
            m_find = (GButton)GetChildAt(17);
            m_tool = (UI_ToolBtn)GetChildAt(18);
            m_auto = (GButton)GetChildAt(19);
            m_help = (GButton)GetChildAt(22);
            m_hp = (UI_FightAttr)GetChildAt(23);
            m_atk = (UI_FightAttr)GetChildAt(24);
            m_power = (GTextField)GetChildAt(27);
            m_eqinfo = (UI_FightEquipInfo)GetChildAt(29);
            m_holder = (GGraph)GetChildAt(32);
            m_mholder = (GGraph)GetChildAt(33);
            m_monster = (GLoader)GetChildAt(34);
            m_battlemonster = (GGraph)GetChildAt(35);
            m_exptips = (GTextField)GetChildAt(36);
            m_fightBtn = (GButton)GetChildAt(37);
            m_fightHp1 = (GProgressBar)GetChildAt(38);
            m_fightHp2 = (GProgressBar)GetChildAt(39);
            m_kill = GetTransitionAt(0);
            m_expanim = GetTransitionAt(1);
            m_showeqinfo = GetTransitionAt(2);
        }
    }
}