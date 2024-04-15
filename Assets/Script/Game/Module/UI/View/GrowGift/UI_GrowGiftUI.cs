/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.GrowGift
{
    public partial class UI_GrowGiftUI : GComponent
    {
        public Controller m_buy;
        public GLabel m_body;
        public GButton m_btnCollect;
        public GButton m_btnBuy;
        public GRichTextField m_lblTime;
        public const string URL = "ui://862omg9yjfxh0";

        public static UI_GrowGiftUI CreateInstance()
        {
            return (UI_GrowGiftUI)UIPackage.CreateObject("GrowGift", "GrowGiftUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_buy = GetControllerAt(0);
            m_body = (GLabel)GetChildAt(0);
            m_btnCollect = (GButton)GetChildAt(1);
            m_btnBuy = (GButton)GetChildAt(2);
            m_lblTime = (GRichTextField)GetChildAt(3);
        }
    }
}