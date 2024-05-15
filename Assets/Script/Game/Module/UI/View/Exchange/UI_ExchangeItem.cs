/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Exchange
{
    public partial class UI_ExchangeItem : GComponent
    {
        public Controller m_state;
        public GLoader m_bg;
        public GLoader m_left;
        public UI_ExchangeProgress m_progress;
        public GTextField m_taskDes;
        public GLoader m_currency;
        public GTextField m_value;
        public const string URL = "ui://d6hhikg5ee8jc";

        public static UI_ExchangeItem CreateInstance()
        {
            return (UI_ExchangeItem)UIPackage.CreateObject("Exchange", "ExchangeItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_state = GetControllerAt(0);
            m_bg = (GLoader)GetChildAt(0);
            m_left = (GLoader)GetChildAt(1);
            m_progress = (UI_ExchangeProgress)GetChildAt(2);
            m_taskDes = (GTextField)GetChildAt(3);
            m_currency = (GLoader)GetChildAt(4);
            m_value = (GTextField)GetChildAt(5);
        }
    }
}