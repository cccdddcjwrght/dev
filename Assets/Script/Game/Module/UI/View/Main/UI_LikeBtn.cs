/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Main
{
    public partial class UI_LikeBtn : GButton
    {
        public Controller m_state;
        public Controller m_markState;
        public GLoader m_progress;
        public GLoader m_mark;
        public GTextField m_time;
        public GTextField m_num;
        public GGroup m_content;
        public GTextField m_info;
        public GGroup m_tip;
        public Transition m_add;
        public Transition m_play;
        public const string URL = "ui://ktixaqlj9ehblb1";

        public static UI_LikeBtn CreateInstance()
        {
            return (UI_LikeBtn)UIPackage.CreateObject("Main", "LikeBtn");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_state = GetControllerAt(1);
            m_markState = GetControllerAt(2);
            m_progress = (GLoader)GetChildAt(1);
            m_mark = (GLoader)GetChildAt(3);
            m_time = (GTextField)GetChildAt(5);
            m_num = (GTextField)GetChildAt(7);
            m_content = (GGroup)GetChildAt(8);
            m_info = (GTextField)GetChildAt(11);
            m_tip = (GGroup)GetChildAt(12);
            m_add = GetTransitionAt(0);
            m_play = GetTransitionAt(1);
        }
    }
}