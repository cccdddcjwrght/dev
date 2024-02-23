/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Common
{
    public partial class UI_Redpoint : GButton
    {
        public Transition m_t0;
        public const string URL = "ui://2w8thcm7tfqd62";

        public static UI_Redpoint CreateInstance()
        {
            return (UI_Redpoint)UIPackage.CreateObject("Common", "Redpoint");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_t0 = GetTransitionAt(0);
        }
    }
}