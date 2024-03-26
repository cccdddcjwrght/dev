/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Player
{
    public partial class UI_EquipPage : GComponent
    {
        public GComponent m_model;
        public GButton m_attrbtn;
        public GTextField m_attr;
        public GGraph m_holder;
        public UI_EqPos m_eq5;
        public UI_EqPos m_eq4;
        public UI_EqPos m_eq1;
        public UI_EqPos m_eq2;
        public UI_EqPos m_eq3;
        public const string URL = "ui://cmw7t1elmk8f1l";

        public static UI_EquipPage CreateInstance()
        {
            return (UI_EquipPage)UIPackage.CreateObject("Player", "EquipPage");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_model = (GComponent)GetChildAt(0);
            m_attrbtn = (GButton)GetChildAt(2);
            m_attr = (GTextField)GetChildAt(3);
            m_holder = (GGraph)GetChildAt(5);
            m_eq5 = (UI_EqPos)GetChildAt(7);
            m_eq4 = (UI_EqPos)GetChildAt(8);
            m_eq1 = (UI_EqPos)GetChildAt(9);
            m_eq2 = (UI_EqPos)GetChildAt(10);
            m_eq3 = (UI_EqPos)GetChildAt(11);
        }
    }
}