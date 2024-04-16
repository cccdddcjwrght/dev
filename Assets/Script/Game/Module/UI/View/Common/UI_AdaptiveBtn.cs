/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Common
{
    public partial class UI_AdaptiveBtn : GButton
    {
        public GLoader m_icon2;
        public const string URL = "ui://2w8thcm7l7ed3ldr";

        public static UI_AdaptiveBtn CreateInstance()
        {
            return (UI_AdaptiveBtn)UIPackage.CreateObject("Common", "AdaptiveBtn");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_icon2 = (GLoader)GetChildAt(1);
        }
    }
}