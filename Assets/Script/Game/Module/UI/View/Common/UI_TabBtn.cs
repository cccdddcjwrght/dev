/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Common
{
    public partial class UI_TabBtn : GButton
    {
        public GGraph m___effect;
        public const string URL = "ui://2w8thcm7n1bm3lbr";

        public static UI_TabBtn CreateInstance()
        {
            return (UI_TabBtn)UIPackage.CreateObject("Common", "TabBtn");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m___effect = (GGraph)GetChildAt(3);
        }
    }
}