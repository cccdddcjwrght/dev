/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.BuffShop
{
    public partial class UI_BuffShopUI : GComponent
    {
        public GLabel m_body;
        public GLoader m_tip;
        public GTextField m_total;
        public GImage m_time_bg;
        public GImage m_time_icon;
        public GTextField m_time;
        public GList m_lotteryList;
        public UI_ShopBuffBtn m_lotteryBtn;
        public GList m_shopBuffList;
        public GGroup m_content;
        public Transition m_t0;
        public const string URL = "ui://ay4imj6tq4m86";

        public static UI_BuffShopUI CreateInstance()
        {
            return (UI_BuffShopUI)UIPackage.CreateObject("BuffShop", "BuffShopUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_body = (GLabel)GetChildAt(0);
            m_tip = (GLoader)GetChildAt(2);
            m_total = (GTextField)GetChildAt(4);
            m_time_bg = (GImage)GetChildAt(9);
            m_time_icon = (GImage)GetChildAt(10);
            m_time = (GTextField)GetChildAt(11);
            m_lotteryList = (GList)GetChildAt(12);
            m_lotteryBtn = (UI_ShopBuffBtn)GetChildAt(13);
            m_shopBuffList = (GList)GetChildAt(15);
            m_content = (GGroup)GetChildAt(18);
            m_t0 = GetTransitionAt(0);
        }
    }
}