/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Rank
{
    public partial class UI_RankTran : GComponent
    {
        public GLoader m_icon;
        public GTextField m___text;
        public GGroup m_content;
        public Transition m_play;
        public const string URL = "ui://k6z01pjbvi8ut";

        public static UI_RankTran CreateInstance()
        {
            return (UI_RankTran)UIPackage.CreateObject("Rank", "RankTran");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_icon = (GLoader)GetChildAt(0);
            m___text = (GTextField)GetChildAt(1);
            m_content = (GGroup)GetChildAt(2);
            m_play = GetTransitionAt(0);
        }
    }
}