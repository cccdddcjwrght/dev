/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.TomorrowGift
{
    public partial class UI_NewbieGiftUI : GComponent
    {
        public GLabel m_body;
        public UI_CommomGift m_gift;
        public GButton m_btnOK;
        public GTextField m_lblPrercent;
        public GComponent m_probablity;
        public const string URL = "ui://7crbg35hk51r7";

        public static UI_NewbieGiftUI CreateInstance()
        {
            return (UI_NewbieGiftUI)UIPackage.CreateObject("TomorrowGift", "NewbieGiftUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_body = (GLabel)GetChildAt(0);
            m_gift = (UI_CommomGift)GetChildAt(1);
            m_btnOK = (GButton)GetChildAt(4);
            m_lblPrercent = (GTextField)GetChildAt(6);
            m_probablity = (GComponent)GetChildAt(8);
        }
    }
}