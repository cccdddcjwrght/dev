/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Common
{
    public partial class UI_CloseBtn : GButton
    {
        public Controller m_Type;
        public const string URL = "ui://2w8thcm7k0s63lb0";

        public static UI_CloseBtn CreateInstance()
        {
            return (UI_CloseBtn)UIPackage.CreateObject("Common", "CloseBtn");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_Type = GetControllerAt(1);
        }
    }
}