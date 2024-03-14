/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Player
{
    public partial class UI_GiftBody : GLabel
    {
        public Controller m_type;
        public GList m_items;
        public GLoader m_icon_open;
        public GButton m_click;
        public Transition m_open;
        public Transition m_t1;
        public const string URL = "ui://cmw7t1elw46k1g";

        public static UI_GiftBody CreateInstance()
        {
            return (UI_GiftBody)UIPackage.CreateObject("Player", "GiftBody");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_type = GetControllerAt(0);
            m_items = (GList)GetChildAt(2);
            m_icon_open = (GLoader)GetChildAt(4);
            m_click = (GButton)GetChildAt(5);
            m_open = GetTransitionAt(0);
            m_t1 = GetTransitionAt(1);
        }
    }
}