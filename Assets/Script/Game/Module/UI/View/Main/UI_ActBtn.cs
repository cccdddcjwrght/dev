/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Main
{
    public partial class UI_ActBtn : GButton
    {
        public Controller m___redpoint;
        public Controller m_timeColor;
        public GGraph m_effect;
        public GImage m_n7;
        public GTextField m_content;
        public GGroup m_time;
        public GLoader m_icon;
        public GImage m_n10;
        public GGroup m_body;
        public Transition m_t0;
        public const string URL = "ui://ktixaqljgmj1w";

        public static UI_ActBtn CreateInstance()
        {
            return (UI_ActBtn)UIPackage.CreateObject("Main", "ActBtn");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m___redpoint = GetControllerAt(0);
            m_timeColor = GetControllerAt(1);
            m_effect = (GGraph)GetChildAt(0);
            m_n7 = (GImage)GetChildAt(1);
            m_content = (GTextField)GetChildAt(2);
            m_time = (GGroup)GetChildAt(3);
            m_icon = (GLoader)GetChildAt(4);
            m_n10 = (GImage)GetChildAt(5);
            m_body = (GGroup)GetChildAt(6);
            m_t0 = GetTransitionAt(0);
        }
    }
}