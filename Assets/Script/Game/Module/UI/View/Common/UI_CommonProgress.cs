/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Common
{
    public partial class UI_CommonProgress : GProgressBar
    {
        public Controller m_state;
        public const string URL = "ui://2w8thcm7mf973lkh";

        public static UI_CommonProgress CreateInstance()
        {
            return (UI_CommonProgress)UIPackage.CreateObject("Common", "CommonProgress");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_state = GetControllerAt(0);
        }
    }
}