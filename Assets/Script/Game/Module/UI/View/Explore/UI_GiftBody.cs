/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Explore
{
    public partial class UI_GiftBody : GLabel
    {
        public Controller m_type;
        public GGraph m___effect;
        public GButton m_click;
        public GList m_list;
        public Transition m_open;
        public Transition m_t1;
        public Transition m_doshow;
        public Transition m_dohide;
        public const string URL = "ui://ow12is1hkv2e2i";

        public static UI_GiftBody CreateInstance()
        {
            return (UI_GiftBody)UIPackage.CreateObject("Explore", "GiftBody");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_type = GetControllerAt(0);
            m___effect = (GGraph)GetChildAt(3);
            m_click = (GButton)GetChildAt(4);
            m_list = (GList)GetChildAt(5);
            m_open = GetTransitionAt(0);
            m_t1 = GetTransitionAt(1);
            m_doshow = GetTransitionAt(2);
            m_dohide = GetTransitionAt(3);
        }
    }
}