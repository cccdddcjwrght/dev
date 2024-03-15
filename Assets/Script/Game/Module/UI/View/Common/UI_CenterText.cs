/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Common
{
    public partial class UI_CenterText : GLabel
    {
        public Controller m_color;
        public Controller m_fontSize;
        public GTextField m_title;
        public const string URL = "ui://2w8thcm7d9cr3l9g";

        public static UI_CenterText CreateInstance()
        {
            return (UI_CenterText)UIPackage.CreateObject("Common", "CenterText");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_color = GetControllerAt(0);
            m_fontSize = GetControllerAt(1);
            m_title = (GTextField)GetChildAt(0);
        }
    }
}