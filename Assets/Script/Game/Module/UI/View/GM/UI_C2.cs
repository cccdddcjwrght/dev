/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.GM
{
    public partial class UI_C2 : GButton
    {
        public Controller m___disablelocal;
        public GLabel m_input;
        public const string URL = "ui://wdfxjeelutmx3";

        public static UI_C2 CreateInstance()
        {
            return (UI_C2)UIPackage.CreateObject("GM", "C2");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m___disablelocal = GetControllerAt(0);
            m_input = (GLabel)GetChildAt(0);
        }
    }
}