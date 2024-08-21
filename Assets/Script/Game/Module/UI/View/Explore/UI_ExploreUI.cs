/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Explore
{
    public partial class UI_ExploreUI : GLabel
    {
        public GLoader m_map;
        public GLabel m_topbar;
        public GButton m_close;
        public UI_ExploreProgress m_progress;
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
        public const string URL = "ui://ow12is1hpm5b14";

        public static UI_ExploreUI CreateInstance()
        {
            return (UI_ExploreUI)UIPackage.CreateObject("Explore", "ExploreUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_map = (GLoader)GetChildAt(0);
            m_topbar = (GLabel)GetChildAt(1);
            m_close = (GButton)GetChildAt(3);
            m_progress = (UI_ExploreProgress)GetChildAt(4);
            m_eq11 = (UI_FightEquip)GetChildAt(8);
            m_eq12 = (UI_FightEquip)GetChildAt(9);
            m_eq13 = (UI_FightEquip)GetChildAt(10);
            m_eq20 = (UI_FightEquip)GetChildAt(11);
            m_find = (GButton)GetChildAt(13);
            m_tool = (UI_ToolBtn)GetChildAt(14);
            m_auto = (GButton)GetChildAt(15);
            m_help = (GButton)GetChildAt(18);
            m_hp = (UI_FightAttr)GetChildAt(19);
            m_atk = (UI_FightAttr)GetChildAt(20);
            m_power = (GTextField)GetChildAt(24);
            m_eqinfo = (UI_FightEquipInfo)GetChildAt(26);
            m_holder = (GGraph)GetChildAt(28);
        }
    }
}