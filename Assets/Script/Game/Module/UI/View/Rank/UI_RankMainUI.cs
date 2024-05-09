/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Rank
{
    public partial class UI_RankMainUI : GComponent
    {
        public Controller m_state;
        public GLabel m_body;
        public GTextField m_time;
        public GTextField m_tip;
        public GList m_list;
        public UI_RankItem m_self;
        public GTextField m_noRank;
        public GGroup m_content;
        public const string URL = "ui://k6z01pjbvi8u3";

        public static UI_RankMainUI CreateInstance()
        {
            return (UI_RankMainUI)UIPackage.CreateObject("Rank", "RankMainUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_state = GetControllerAt(0);
            m_body = (GLabel)GetChildAt(0);
            m_time = (GTextField)GetChildAt(4);
            m_tip = (GTextField)GetChildAt(6);
            m_list = (GList)GetChildAt(7);
            m_self = (UI_RankItem)GetChildAt(9);
            m_noRank = (GTextField)GetChildAt(10);
            m_content = (GGroup)GetChildAt(13);
        }
    }
}