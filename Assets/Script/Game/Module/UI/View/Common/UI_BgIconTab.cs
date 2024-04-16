/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Common
{
    public partial class UI_BgIconTab : GButton
    {
        public GLoader m_bg;
        public GLoader m_icon1;
        public const string URL = "ui://2w8thcm7l7ed3ldu";

        public static UI_BgIconTab CreateInstance()
        {
            return (UI_BgIconTab)UIPackage.CreateObject("Common", "BgIconTab");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_bg = (GLoader)GetChildAt(0);
            m_icon1 = (GLoader)GetChildAt(2);
        }
    }
}