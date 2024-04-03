/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Main
{
    public partial class UI_ShowBtn : GButton
    {
        public Controller m_ctrlTime;
        public GGraph m_effect;
        public GGroup m_body;
        public GTextField m_content;
        public GGroup m_time;
        public Transition m_t0;
        public const string URL = "ui://ktixaqljk0s6las";

        public static UI_ShowBtn CreateInstance()
        {
            return (UI_ShowBtn)UIPackage.CreateObject("Main", "ShowBtn");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_ctrlTime = GetControllerAt(0);
            m_effect = (GGraph)GetChildAt(0);
            m_body = (GGroup)GetChildAt(3);
            m_content = (GTextField)GetChildAt(5);
            m_time = (GGroup)GetChildAt(6);
            m_t0 = GetTransitionAt(0);
        }
    }
}