/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.TomorrowGift
{
    public partial class UI_TomorrowGiftUI : GComponent
    {
        public GLabel m_body;
        public GButton m_btnOK;
        public UI_Item m_item1;
        public UI_Item m_item2;
        public GTextField m_timelabel;
        public GTextField m_time;
        public GGroup m_timegroup;
        public GComponent m_probablity;
        public const string URL = "ui://7crbg35hosc40";

        public static UI_TomorrowGiftUI CreateInstance()
        {
            return (UI_TomorrowGiftUI)UIPackage.CreateObject("TomorrowGift", "TomorrowGiftUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_body = (GLabel)GetChildAt(0);
            m_btnOK = (GButton)GetChildAt(1);
            m_item1 = (UI_Item)GetChildAt(3);
            m_item2 = (UI_Item)GetChildAt(4);
            m_timelabel = (GTextField)GetChildAt(6);
            m_time = (GTextField)GetChildAt(7);
            m_timegroup = (GGroup)GetChildAt(8);
            m_probablity = (GComponent)GetChildAt(10);
        }
    }
}