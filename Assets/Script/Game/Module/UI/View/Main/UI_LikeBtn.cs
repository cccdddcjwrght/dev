/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Main
{
    public partial class UI_LikeBtn : GButton
    {
        public Controller m_state;
        public GTextField m_info;
        public GGroup m_tipGroup;
        public GLoader m_progress;
        public GTextField m_time;
        public GTextField m_num;
        public GGroup m_content;
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
            m_info = (GTextField)GetChildAt(1);
            m_tipGroup = (GGroup)GetChildAt(2);
            m_progress = (GLoader)GetChildAt(4);
            m_time = (GTextField)GetChildAt(7);
            m_num = (GTextField)GetChildAt(9);
            m_content = (GGroup)GetChildAt(10);
            m_add = GetTransitionAt(0);
            m_play = GetTransitionAt(1);
        }
    }
}