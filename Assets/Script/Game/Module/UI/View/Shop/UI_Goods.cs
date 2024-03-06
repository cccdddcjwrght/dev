/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Shop
{
    public partial class UI_Goods : GButton
    {
        public Controller m_currency;
        public Controller m_cd;
        public Controller m_hidebottom;
        public Controller m_item;
        public Controller m_saled;
        public Controller m_type;
        public UI_shopclick m_click;
        public GImage m_bottom;
        public GLoader m_currency_2;
        public GLoader m_item_2;
        public GTextField m_price;
        public GTextField m_desc;
        public GTextField m_time;
        public GButton m_tips;
        public const string URL = "ui://aphwhwgnlxylq";

        public static UI_Goods CreateInstance()
        {
            return (UI_Goods)UIPackage.CreateObject("Shop", "Goods");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_currency = GetControllerAt(0);
            m_cd = GetControllerAt(1);
            m_hidebottom = GetControllerAt(2);
            m_item = GetControllerAt(3);
            m_saled = GetControllerAt(4);
            m_type = GetControllerAt(5);
            m_click = (UI_shopclick)GetChildAt(1);
            m_bottom = (GImage)GetChildAt(2);
            m_currency_2 = (GLoader)GetChildAt(3);
            m_item_2 = (GLoader)GetChildAt(4);
            m_price = (GTextField)GetChildAt(6);
            m_desc = (GTextField)GetChildAt(8);
            m_time = (GTextField)GetChildAt(9);
            m_tips = (GButton)GetChildAt(10);
        }
    }
}