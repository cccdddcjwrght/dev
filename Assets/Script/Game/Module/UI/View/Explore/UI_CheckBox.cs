/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Explore
{
    public partial class UI_CheckBox : GButton
    {
        public Controller m_type;
        public Controller m_hidebg;
        public const string URL = "ui://ow12is1hpiyq3g";

        public static UI_CheckBox CreateInstance()
        {
            return (UI_CheckBox)UIPackage.CreateObject("Explore", "CheckBox");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_type = GetControllerAt(0);
            m_hidebg = GetControllerAt(1);
        }
    }
}