/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Rank
{
    public partial class UI_RankItem : GComponent
    {
        public Controller m_rankIndex;
        public GLoader m_bg;
        public GLoader m_rankIcon;
        public GTextField m_rank;
        public GButton m_head;
        public GTextField m_name;
        public GLoader m_tag;
        public GList m_list;
        public GTextField m_value;
        public GGroup m_content;
        public const string URL = "ui://k6z01pjbvi8ul";

        public static UI_RankItem CreateInstance()
        {
            return (UI_RankItem)UIPackage.CreateObject("Rank", "RankItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_rankIndex = GetControllerAt(0);
            m_bg = (GLoader)GetChildAt(0);
            m_rankIcon = (GLoader)GetChildAt(1);
            m_rank = (GTextField)GetChildAt(2);
            m_head = (GButton)GetChildAt(3);
            m_name = (GTextField)GetChildAt(4);
            m_tag = (GLoader)GetChildAt(5);
            m_list = (GList)GetChildAt(6);
            m_value = (GTextField)GetChildAt(8);
            m_content = (GGroup)GetChildAt(9);
        }
    }
}