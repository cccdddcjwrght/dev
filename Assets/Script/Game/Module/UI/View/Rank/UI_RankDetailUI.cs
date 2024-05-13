/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Rank
{
    public partial class UI_RankDetailUI : GComponent
    {
        public GButton m_btnClose;
        public GTextField m_title;
        public GComponent m_Equip;
        public GGroup m_content;
        public const string URL = "ui://k6z01pjbvi8uq";

        public static UI_RankDetailUI CreateInstance()
        {
            return (UI_RankDetailUI)UIPackage.CreateObject("Rank", "RankDetailUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_btnClose = (GButton)GetChildAt(1);
            m_title = (GTextField)GetChildAt(2);
            m_Equip = (GComponent)GetChildAt(3);
            m_content = (GGroup)GetChildAt(4);
        }
    }
}