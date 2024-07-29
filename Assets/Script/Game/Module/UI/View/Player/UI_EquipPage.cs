/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Player
{
    public partial class UI_EquipPage : GComponent
    {
        public Controller m_bg;
        public Controller m_pos;
        public UI_bgclick m_model;
        public GButton m_attrbtn;
        public GTextField m_attr;
        public GGraph m_holder;
        public GGraph m_modeleffect;
        public UI_EqPos m_eq1;
        public UI_EqPos m_eq2;
        public UI_EqPos m_eq3;
        public UI_Equip m_eq5;
        public UI_Equip m_eq6;
        public UI_Equip m_eq4;
        public GGraph m_upeffect;
        public const string URL = "ui://cmw7t1elmk8f1l";

        public static UI_EquipPage CreateInstance()
        {
            return (UI_EquipPage)UIPackage.CreateObject("Player", "EquipPage");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_bg = GetControllerAt(0);
            m_pos = GetControllerAt(1);
            m_model = (UI_bgclick)GetChildAt(0);
            m_attrbtn = (GButton)GetChildAt(1);
            m_attr = (GTextField)GetChildAt(4);
            m_holder = (GGraph)GetChildAt(7);
            m_modeleffect = (GGraph)GetChildAt(8);
            m_eq1 = (UI_EqPos)GetChildAt(11);
            m_eq2 = (UI_EqPos)GetChildAt(12);
            m_eq3 = (UI_EqPos)GetChildAt(13);
            m_eq5 = (UI_Equip)GetChildAt(15);
            m_eq6 = (UI_Equip)GetChildAt(16);
            m_eq4 = (UI_Equip)GetChildAt(17);
            m_upeffect = (GGraph)GetChildAt(20);
        }
    }
}