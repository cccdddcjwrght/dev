/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.BuffShop
{
    public partial class UI_ShopBuffBtn : GButton
    {
        public Controller m_currency;
        public Controller m_cd;
        public Controller m_saled;
        public UI_shopclick m_click;
        public GLoader m_currency_2;
        public GTextField m_price;
        public GTextField m_time;
        public const string URL = "ui://ay4imj6tpvwqa";

        public static UI_ShopBuffBtn CreateInstance()
        {
            return (UI_ShopBuffBtn)UIPackage.CreateObject("BuffShop", "ShopBuffBtn");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_currency = GetControllerAt(0);
            m_cd = GetControllerAt(1);
            m_saled = GetControllerAt(2);
            m_click = (UI_shopclick)GetChildAt(0);
            m_currency_2 = (GLoader)GetChildAt(1);
            m_price = (GTextField)GetChildAt(2);
            m_time = (GTextField)GetChildAt(3);
        }
    }
}