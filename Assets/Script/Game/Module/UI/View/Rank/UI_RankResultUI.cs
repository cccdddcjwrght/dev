/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Rank
{
    public partial class UI_RankResultUI : GComponent
    {
        public GTextField m_title;
        public GList m_list;
        public GTextField m_tip;
        public GGroup m_content;
        public const string URL = "ui://k6z01pjbvi8uo";

        public static UI_RankResultUI CreateInstance()
        {
            return (UI_RankResultUI)UIPackage.CreateObject("Rank", "RankResultUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_title = (GTextField)GetChildAt(3);
            m_list = (GList)GetChildAt(4);
            m_tip = (GTextField)GetChildAt(5);
            m_content = (GGroup)GetChildAt(6);
        }
    }
}