/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.GM
{
    public partial class UI_C1 : GButton
    {
        public Controller m___disablelocal;
        public const string URL = "ui://wdfxjeelutmx1";

        public static UI_C1 CreateInstance()
        {
            return (UI_C1)UIPackage.CreateObject("GM", "C1");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m___disablelocal = GetControllerAt(1);
        }
    }
}