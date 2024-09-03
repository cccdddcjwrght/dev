/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.DailyTask
{
    public partial class UI_DailyTaskUI : GComponent
    {
        public GLabel m_body;
        public GTextField m_dayValue;
        public GImage m_barbg;
        public GImage m_bar;
        public UI_DailyGiftItem m_giftItem1;
        public UI_DailyGiftItem m_giftItem2;
        public UI_DailyGiftItem m_giftItem3;
        public UI_DailyGiftItem m_giftItem4;
        public UI_DailyGiftItem m_giftItem5;
        public GList m_taskList;
        public GTextField m_dayTime;
        public GTextField m_weekTime;
        public GTextField m_weekValue;
        public GList m_rewardList;
        public GGroup m_content;
        public const string URL = "ui://ti6n81b9diea1";

        public static UI_DailyTaskUI CreateInstance()
        {
            return (UI_DailyTaskUI)UIPackage.CreateObject("DailyTask", "DailyTaskUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_body = (GLabel)GetChildAt(0);
            m_dayValue = (GTextField)GetChildAt(3);
            m_barbg = (GImage)GetChildAt(5);
            m_bar = (GImage)GetChildAt(6);
            m_giftItem1 = (UI_DailyGiftItem)GetChildAt(7);
            m_giftItem2 = (UI_DailyGiftItem)GetChildAt(8);
            m_giftItem3 = (UI_DailyGiftItem)GetChildAt(9);
            m_giftItem4 = (UI_DailyGiftItem)GetChildAt(10);
            m_giftItem5 = (UI_DailyGiftItem)GetChildAt(11);
            m_taskList = (GList)GetChildAt(12);
            m_dayTime = (GTextField)GetChildAt(17);
            m_weekTime = (GTextField)GetChildAt(20);
            m_weekValue = (GTextField)GetChildAt(24);
            m_rewardList = (GList)GetChildAt(25);
            m_content = (GGroup)GetChildAt(26);
        }
    }
}