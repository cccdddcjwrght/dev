/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Setting
{
    public partial class UI_SwitchBtn : GButton
    {
        public Controller m___disablelocal;
        public const string URL = "ui://dpgt0e2zn1bmv";

        public static UI_SwitchBtn CreateInstance()
        {
            return (UI_SwitchBtn)UIPackage.CreateObject("Setting", "SwitchBtn");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m___disablelocal = GetControllerAt(1);
        }
    }
}