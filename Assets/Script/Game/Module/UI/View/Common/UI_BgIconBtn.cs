/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Common
{
    public partial class UI_BgIconBtn : GButton
    {
        public GLoader m_bg;
        public const string URL = "ui://2w8thcm7l7ed3lds";

        public static UI_BgIconBtn CreateInstance()
        {
            return (UI_BgIconBtn)UIPackage.CreateObject("Common", "BgIconBtn");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_bg = (GLoader)GetChildAt(0);
        }
    }
}