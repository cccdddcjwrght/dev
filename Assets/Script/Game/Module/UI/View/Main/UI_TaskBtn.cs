/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Main
{
    public partial class UI_TaskBtn : GButton
    {
        public Controller m_state;
        public Controller m_finish;
        public GLoader m_click;
        public GGroup m_group;
        public Transition m_show;
        public Transition m_hide;
        public const string URL = "ui://ktixaqljsp7elbq";

        public static UI_TaskBtn CreateInstance()
        {
            return (UI_TaskBtn)UIPackage.CreateObject("Main", "TaskBtn");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_state = GetControllerAt(0);
            m_finish = GetControllerAt(1);
            m_click = (GLoader)GetChildAt(0);
            m_group = (GGroup)GetChildAt(2);
            m_show = GetTransitionAt(0);
            m_hide = GetTransitionAt(1);
        }
    }
}