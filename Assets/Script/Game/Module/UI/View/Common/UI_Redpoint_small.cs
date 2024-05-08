/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Common
{
    public partial class UI_Redpoint_small : GButton
    {
        public Transition m_t0;
        public const string URL = "ui://2w8thcm7topc3lew";

        public static UI_Redpoint_small CreateInstance()
        {
            return (UI_Redpoint_small)UIPackage.CreateObject("Common", "Redpoint_small");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_t0 = GetTransitionAt(0);
        }
    }
}