/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Main
{
    public partial class UI_ActBtn : GButton
    {
        public Controller m___redpoint;
        public Controller m_timeColor;
        public Controller m_ctrlTime;
        public Controller m_side;
        public GGraph m_effect;
        public GGroup m_body;
        public GButton m_redpoint;
        public GTextField m_content;
        public GGroup m_time;
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
            m_ctrlTime = GetControllerAt(2);
            m_side = GetControllerAt(3);
            m_effect = (GGraph)GetChildAt(0);
            m_body = (GGroup)GetChildAt(4);
            m_redpoint = (GButton)GetChildAt(5);
            m_content = (GTextField)GetChildAt(7);
            m_time = (GGroup)GetChildAt(8);
            m_t0 = GetTransitionAt(0);
        }
    }
}