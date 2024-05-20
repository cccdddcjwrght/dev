/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Exchange
{
    public partial class UI_ExchangeTaskUI : GComponent
    {
        public Controller m_type;
        public GLabel m_body;
        public GLoader m_currency;
        public GTextField m_value;
        public GButton m_tab1;
        public GButton m_tab2;
        public GButton m_reddot1;
        public GButton m_reddot2;
        public GList m_taskList;
        public GList m_goodList;
        public GTextField m_time;
        public GGroup m_content;
        public const string URL = "ui://d6hhikg5ee8jb";

        public static UI_ExchangeTaskUI CreateInstance()
        {
            return (UI_ExchangeTaskUI)UIPackage.CreateObject("Exchange", "ExchangeTaskUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_type = GetControllerAt(0);
            m_body = (GLabel)GetChildAt(0);
            m_currency = (GLoader)GetChildAt(2);
            m_value = (GTextField)GetChildAt(3);
            m_tab1 = (GButton)GetChildAt(4);
            m_tab2 = (GButton)GetChildAt(5);
            m_reddot1 = (GButton)GetChildAt(6);
            m_reddot2 = (GButton)GetChildAt(7);
            m_taskList = (GList)GetChildAt(9);
            m_goodList = (GList)GetChildAt(10);
            m_time = (GTextField)GetChildAt(11);
            m_content = (GGroup)GetChildAt(12);
        }
    }
}