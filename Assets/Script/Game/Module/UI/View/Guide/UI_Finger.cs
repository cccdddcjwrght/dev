/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Guide
{
    public partial class UI_Finger : GComponent
    {
        public GImage m_n1;
        public GImage m_Finger;
        public Transition m_shouzhi;
        public const string URL = "ui://hebbif0xk4sr2";

        public static UI_Finger CreateInstance()
        {
            return (UI_Finger)UIPackage.CreateObject("Guide", "Finger");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_n1 = (GImage)GetChildAt(0);
            m_Finger = (GImage)GetChildAt(1);
            m_shouzhi = GetTransitionAt(0);
        }
    }
}