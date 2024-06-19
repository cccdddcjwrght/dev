/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Main
{
    public partial class UI_LikeBtn : GButton
    {
        public GLoader m_progress;
        public GLoader m_mark;
        public GTextField m_num;
        public GTextField m_count;
        public GGroup m_count_group;
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

            m_progress = (GLoader)GetChildAt(1);
            m_mark = (GLoader)GetChildAt(3);
            m_num = (GTextField)GetChildAt(4);
            m_count = (GTextField)GetChildAt(6);
            m_count_group = (GGroup)GetChildAt(7);
            m_content = (GGroup)GetChildAt(8);
            m_info = (GTextField)GetChildAt(11);
            m_tip = (GGroup)GetChildAt(12);
            m_add = GetTransitionAt(0);
            m_play = GetTransitionAt(1);
        }
    }
}