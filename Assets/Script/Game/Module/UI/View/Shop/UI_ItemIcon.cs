/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Shop
{
    public partial class UI_ItemIcon : GButton
    {
        public GImage m_bg;
        public const string URL = "ui://aphwhwgnlxylx";

        public static UI_ItemIcon CreateInstance()
        {
            return (UI_ItemIcon)UIPackage.CreateObject("Shop", "ItemIcon");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_bg = (GImage)GetChildAt(0);
        }
    }
}