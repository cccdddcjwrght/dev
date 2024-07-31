/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Main
{
    public partial class UI_InvestMan : GButton
    {
        public GImage m_bar;
        public Transition m_t0;
        public const string URL = "ui://ktixaqlju2orlb9";

        public static UI_InvestMan CreateInstance()
        {
            return (UI_InvestMan)UIPackage.CreateObject("Main", "InvestMan");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_bar = (GImage)GetChildAt(1);
            m_t0 = GetTransitionAt(0);
        }
    }
}