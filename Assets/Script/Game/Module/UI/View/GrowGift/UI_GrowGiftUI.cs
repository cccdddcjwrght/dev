/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.GrowGift
{
    public partial class UI_GrowGiftUI : GComponent
    {
        public GLabel m_body;
        public const string URL = "ui://862omg9yjfxh0";

        public static UI_GrowGiftUI CreateInstance()
        {
            return (UI_GrowGiftUI)UIPackage.CreateObject("GrowGift", "GrowGiftUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_body = (GLabel)GetChildAt(0);
        }
    }
}