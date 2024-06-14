/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Player
{
    public partial class UI_EquipUpQuality : GComponent
    {
        public Controller m_state;
        public Controller m_type;
        public UI_Equip m_nexteq;
        public GButton m_click;
        public UI_attrlabel m_addeffect;
        public GList m_list;
        public GTextField m_nextattr;
        public GTextField m_curattr;
        public GGraph m___effect;
        public const string URL = "ui://cmw7t1elmk8f1m";

        public static UI_EquipUpQuality CreateInstance()
        {
            return (UI_EquipUpQuality)UIPackage.CreateObject("Player", "EquipUpQuality");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_state = GetControllerAt(0);
            m_type = GetControllerAt(1);
            m_nexteq = (UI_Equip)GetChildAt(3);
            m_click = (GButton)GetChildAt(4);
            m_addeffect = (UI_attrlabel)GetChildAt(5);
            m_list = (GList)GetChildAt(6);
            m_nextattr = (GTextField)GetChildAt(10);
            m_curattr = (GTextField)GetChildAt(11);
            m___effect = (GGraph)GetChildAt(20);
        }
    }
}