/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Flight
{
    public partial class UI_RankTran : GLabel
    {
        public GTextField m___text;
        public GGroup m_content;
        public Transition m_play;
        public const string URL = "ui://kh4ukj1qwtgn1";

        public static UI_RankTran CreateInstance()
        {
            return (UI_RankTran)UIPackage.CreateObject("Flight", "RankTran");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m___text = (GTextField)GetChildAt(1);
            m_content = (GGroup)GetChildAt(2);
            m_play = GetTransitionAt(0);
        }
    }
}