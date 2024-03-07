/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Player
{
    public partial class UI_PlayerUI : GLabel
    {
        public Controller m_eqTab;
        public GLabel m_body;
        public GComponent m_model;
        public GButton m_attrbtn;
        public GTextField m_attr;
        public GGraph m_holder;
        public UI_EqPos m_eq1;
        public UI_EqPos m_eq2;
        public UI_EqPos m_eq3;
        public UI_EqPos m_eq4;
        public UI_EqPos m_eq5;
        public GList m_tabs;
        public GList m_list;
        public const string URL = "ui://cmw7t1elk6220";

        public static UI_PlayerUI CreateInstance()
        {
            return (UI_PlayerUI)UIPackage.CreateObject("Player", "PlayerUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_eqTab = GetControllerAt(0);
            m_body = (GLabel)GetChildAt(0);
            m_model = (GComponent)GetChildAt(1);
            m_attrbtn = (GButton)GetChildAt(4);
            m_attr = (GTextField)GetChildAt(5);
            m_holder = (GGraph)GetChildAt(7);
            m_eq1 = (UI_EqPos)GetChildAt(9);
            m_eq2 = (UI_EqPos)GetChildAt(10);
            m_eq3 = (UI_EqPos)GetChildAt(11);
            m_eq4 = (UI_EqPos)GetChildAt(12);
            m_eq5 = (UI_EqPos)GetChildAt(13);
            m_tabs = (GList)GetChildAt(14);
            m_list = (GList)GetChildAt(15);
        }
    }
}