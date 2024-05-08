/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Shop
{
    public partial class UI_ShopBody : GComponent
    {
        public Controller m_pages;
        public Controller m_hidead;
        public UI_BigGoods m_adgood;
        public GList m_gifts;
        public GList m_page;
        public GList m_goods;
        public const string URL = "ui://aphwhwgnlxylp";

        public static UI_ShopBody CreateInstance()
        {
            return (UI_ShopBody)UIPackage.CreateObject("Shop", "ShopBody");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_pages = GetControllerAt(0);
            m_hidead = GetControllerAt(1);
            m_adgood = (UI_BigGoods)GetChildAt(0);
            m_gifts = (GList)GetChildAt(1);
            m_page = (GList)GetChildAt(2);
            m_goods = (GList)GetChildAt(4);
        }
    }
}