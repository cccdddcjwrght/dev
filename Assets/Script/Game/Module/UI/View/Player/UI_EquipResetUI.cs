/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Player
{
    public partial class UI_EquipResetUI : GLabel
    {
        public Controller m_func;
        public GLabel m_body;
        public UI_Equip m_equip;
        public GButton m_click;
        public GButton m_click2;
        public GList m_list;
        public const string URL = "ui://cmw7t1elwaj63z";

        public static UI_EquipResetUI CreateInstance()
        {
            return (UI_EquipResetUI)UIPackage.CreateObject("Player", "EquipResetUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_func = GetControllerAt(0);
            m_body = (GLabel)GetChildAt(0);
            m_equip = (UI_Equip)GetChildAt(5);
            m_click = (GButton)GetChildAt(7);
            m_click2 = (GButton)GetChildAt(8);
            m_list = (GList)GetChildAt(9);
        }
    }
}