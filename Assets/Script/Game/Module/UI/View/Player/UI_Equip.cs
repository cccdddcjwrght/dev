/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Player
{
    public partial class UI_Equip : GButton
    {
        public Controller m_quality;
        public Controller m_eq;
        public Controller m_state;
        public Controller m_part;
        public Controller m_rightbottom;
        public Controller m_lvstate;
        public Controller m_select;
        public Controller m_type;
        public Controller m_suitmat;
        public Controller m_mask;
        public GImage m_bg;
        public GLoader m_quality_2;
        public GTextField m_level;
        public GLoader m___icon;
        public GTextField m_count;
        public const string URL = "ui://cmw7t1elk62213";

        public static UI_Equip CreateInstance()
        {
            return (UI_Equip)UIPackage.CreateObject("Player", "Equip");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_quality = GetControllerAt(0);
            m_eq = GetControllerAt(1);
            m_state = GetControllerAt(2);
            m_part = GetControllerAt(3);
            m_rightbottom = GetControllerAt(4);
            m_lvstate = GetControllerAt(5);
            m_select = GetControllerAt(6);
            m_type = GetControllerAt(7);
            m_suitmat = GetControllerAt(8);
            m_mask = GetControllerAt(9);
            m_bg = (GImage)GetChildAt(0);
            m_quality_2 = (GLoader)GetChildAt(1);
            m_level = (GTextField)GetChildAt(4);
            m___icon = (GLoader)GetChildAt(9);
            m_count = (GTextField)GetChildAt(10);
        }
    }
}