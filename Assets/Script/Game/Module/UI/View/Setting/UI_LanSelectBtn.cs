/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Setting
{
    public partial class UI_LanSelectBtn : GButton
    {
        public Controller m_language;
        public const string URL = "ui://dpgt0e2zfpxr1d";

        public static UI_LanSelectBtn CreateInstance()
        {
            return (UI_LanSelectBtn)UIPackage.CreateObject("Setting", "LanSelectBtn");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_language = GetControllerAt(1);
        }
    }
}