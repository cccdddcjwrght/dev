/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.GM
{
    public partial class UI_C3 : GLabel
    {
        public Controller m___disablelocal;
        public GLabel m_input;
        public GComboBox m_select;
        public const string URL = "ui://wdfxjeelutmx7";

        public static UI_C3 CreateInstance()
        {
            return (UI_C3)UIPackage.CreateObject("GM", "C3");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m___disablelocal = GetControllerAt(0);
            m_input = (GLabel)GetChildAt(1);
            m_select = (GComboBox)GetChildAt(2);
        }
    }
}