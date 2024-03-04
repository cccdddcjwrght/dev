/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Common
{
    public partial class UI_MaskUI : GComponent
    {
        public GGraph m_bg;
        public const string URL = "ui://2w8thcm7n1bm3lbt";

        public static UI_MaskUI CreateInstance()
        {
            return (UI_MaskUI)UIPackage.CreateObject("Common", "MaskUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_bg = (GGraph)GetChildAt(0);
        }
    }
}