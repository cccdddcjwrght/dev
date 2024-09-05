/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Common
{
    public partial class UI_CurrencyHead : GLabel
    {
        public Controller m_currency;
        public Controller m_closetype;
        public Controller m_type;
        public Controller m_shopred;
        public UI_CloseBtn m_close;
        public UI_Currency m_gold;
        public UI_Currency m_diamond;
        public UI_HeadBtn m_head;
        public GButton m_shop;
        public const string URL = "ui://2w8thcm7zsf43lkv";

        public static UI_CurrencyHead CreateInstance()
        {
            return (UI_CurrencyHead)UIPackage.CreateObject("Common", "CurrencyHead");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_currency = GetControllerAt(0);
            m_closetype = GetControllerAt(1);
            m_type = GetControllerAt(2);
            m_shopred = GetControllerAt(3);
            m_close = (UI_CloseBtn)GetChildAt(1);
            m_gold = (UI_Currency)GetChildAt(2);
            m_diamond = (UI_Currency)GetChildAt(3);
            m_head = (UI_HeadBtn)GetChildAt(5);
            m_shop = (GButton)GetChildAt(6);
        }
    }
}