/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Explore
{
    public partial class UI_ToolBtn : GButton
    {
        public Controller m_type;
        public Controller m_state;
        public Controller m_bgcolor;
        public Controller m_showcd;
        public GImage m_bg1;
        public GImage m_bg;
        public GImage m_bg2;
        public GImage m_cd;
        public GTextField m_time;
        public Transition m_t0;
        public const string URL = "ui://ow12is1hpm5b15";

        public static UI_ToolBtn CreateInstance()
        {
            return (UI_ToolBtn)UIPackage.CreateObject("Explore", "ToolBtn");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_type = GetControllerAt(0);
            m_state = GetControllerAt(1);
            m_bgcolor = GetControllerAt(2);
            m_showcd = GetControllerAt(3);
            m_bg1 = (GImage)GetChildAt(0);
            m_bg = (GImage)GetChildAt(1);
            m_bg2 = (GImage)GetChildAt(2);
            m_cd = (GImage)GetChildAt(4);
            m_time = (GTextField)GetChildAt(8);
            m_t0 = GetTransitionAt(0);
        }
    }
}