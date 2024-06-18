/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Player
{
    public partial class UI_EqPos : GButton
    {
        public Controller m_state;
        public Controller m___redpoint;
        public UI_Equip m_body;
        public GTextField m_attr;
        public GLoader m_upclick;
        public GLoader m_currency;
        public const string URL = "ui://cmw7t1elss9b4a";

        public static UI_EqPos CreateInstance()
        {
            return (UI_EqPos)UIPackage.CreateObject("Player", "EqPos");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_state = GetControllerAt(0);
            m___redpoint = GetControllerAt(1);
            m_body = (UI_Equip)GetChildAt(0);
            m_attr = (GTextField)GetChildAt(6);
            m_upclick = (GLoader)GetChildAt(7);
            m_currency = (GLoader)GetChildAt(8);
        }
    }
}