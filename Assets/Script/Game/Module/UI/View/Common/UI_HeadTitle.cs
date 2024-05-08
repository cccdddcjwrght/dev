/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Common
{
    public partial class UI_HeadTitle : GLabel
    {
        public GButton m_close;
        public const string URL = "ui://2w8thcm7e9cj14";

        public static UI_HeadTitle CreateInstance()
        {
            return (UI_HeadTitle)UIPackage.CreateObject("Common", "HeadTitle");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_close = (GButton)GetChildAt(2);
        }
    }
}