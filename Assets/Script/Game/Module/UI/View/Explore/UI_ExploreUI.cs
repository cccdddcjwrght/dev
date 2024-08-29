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
        public GButton m_warn;
        public GTextField m_round;
        public GGroup m_roundGroup;
        public UI_FightEquip m_eq11;
        public UI_FightEquip m_eq12;
        public UI_FightEquip m_eq13;
        public UI_FightEquip m_eq20;
        public GButton m_find;
        public UI_ToolBtn m_tool;
        public UI_ToolBtn m_auto;
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
        public UI_FightHp m_fightHp1;
        public UI_FightHp m_fightHp2;
        public Transition m_kill;
        public Transition m_expanim;
        public Transition m_showeqinfo;
        public Transition m_t3;
        public Transition m_t4;
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
            m_warn = (GButton)GetChildAt(5);
            m_round = (GTextField)GetChildAt(7);
            m_roundGroup = (GGroup)GetChildAt(8);
            m_eq11 = (UI_FightEquip)GetChildAt(13);
            m_eq12 = (UI_FightEquip)GetChildAt(14);
            m_eq13 = (UI_FightEquip)GetChildAt(15);
            m_eq20 = (UI_FightEquip)GetChildAt(16);
            m_find = (GButton)GetChildAt(18);
            m_tool = (UI_ToolBtn)GetChildAt(19);
            m_auto = (UI_ToolBtn)GetChildAt(20);
            m_help = (GButton)GetChildAt(23);
            m_hp = (UI_FightAttr)GetChildAt(24);
            m_atk = (UI_FightAttr)GetChildAt(25);
            m_power = (GTextField)GetChildAt(28);
            m_eqinfo = (UI_FightEquipInfo)GetChildAt(31);
            m_holder = (GGraph)GetChildAt(34);
            m_mholder = (GGraph)GetChildAt(35);
            m_monster = (GLoader)GetChildAt(36);
            m_battlemonster = (GGraph)GetChildAt(38);
            m_exptips = (GTextField)GetChildAt(39);
            m_fightBtn = (GButton)GetChildAt(40);
            m_fightHp1 = (UI_FightHp)GetChildAt(41);
            m_fightHp2 = (UI_FightHp)GetChildAt(42);
            m_kill = GetTransitionAt(0);
            m_expanim = GetTransitionAt(1);
            m_showeqinfo = GetTransitionAt(2);
            m_t3 = GetTransitionAt(3);
            m_t4 = GetTransitionAt(4);
        }
    }
}