/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Setting
{
    public partial class UI_ChangeHeadUI : GComponent
    {
        public Controller m_State;
        public GLabel m_body;
        public GList m_list;
        public GButton m_head;
        public GButton m_frame;
        public GButton m_icon;
        public const string URL = "ui://dpgt0e2zn1bm20";

        public static UI_ChangeHeadUI CreateInstance()
        {
            return (UI_ChangeHeadUI)UIPackage.CreateObject("Setting", "ChangeHeadUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_State = GetControllerAt(0);
            m_body = (GLabel)GetChildAt(0);
            m_list = (GList)GetChildAt(3);
            m_head = (GButton)GetChildAt(4);
            m_frame = (GButton)GetChildAt(5);
            m_icon = (GButton)GetChildAt(7);
        }
    }
}