/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Main
{
    public partial class UI_BuffBtn : GButton
    {
        public Controller m_markState;
        public Controller m_isTime;
        public Controller m_tipState;
        public GLoader m_mark;
        public GTextField m_time;
        public GTextField m_info;
        public GGroup m_tip;
        public const string URL = "ui://ktixaqljggghlb0";

        public static UI_BuffBtn CreateInstance()
        {
            return (UI_BuffBtn)UIPackage.CreateObject("Main", "BuffBtn");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_markState = GetControllerAt(1);
            m_isTime = GetControllerAt(2);
            m_tipState = GetControllerAt(3);
            m_mark = (GLoader)GetChildAt(3);
            m_time = (GTextField)GetChildAt(4);
            m_info = (GTextField)GetChildAt(6);
            m_tip = (GGroup)GetChildAt(7);
        }
    }
}