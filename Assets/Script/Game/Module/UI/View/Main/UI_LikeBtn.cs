/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Main
{
    public partial class UI_LikeBtn : GButton
    {
        public GTextField m_num;
        public GTextField m_count;
        public Transition m_add;
        public Transition m_play;
        public Transition m_zan;
        public const string URL = "ui://ktixaqlj9ehblb1";

        public static UI_LikeBtn CreateInstance()
        {
            return (UI_LikeBtn)UIPackage.CreateObject("Main", "LikeBtn");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_num = (GTextField)GetChildAt(2);
            m_count = (GTextField)GetChildAt(3);
            m_add = GetTransitionAt(0);
            m_play = GetTransitionAt(1);
            m_zan = GetTransitionAt(2);
        }
    }
}