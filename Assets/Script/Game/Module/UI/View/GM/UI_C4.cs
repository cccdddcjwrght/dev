/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.GM
{
    public partial class UI_C4 : GLabel
    {
        public Controller m___disablelocal;
        public GComboBox m_select;
        public const string URL = "ui://wdfxjeelutmx8";

        public static UI_C4 CreateInstance()
        {
            return (UI_C4)UIPackage.CreateObject("GM", "C4");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m___disablelocal = GetControllerAt(0);
            m_select = (GComboBox)GetChildAt(1);
        }
    }
}