/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.BuffShop
{
    public partial class UI_BuffShopItem : GLabel
    {
        public GImage m_time_bg;
        public GTextField m_time;
        public GTextField m_value;
        public GTextField m_des;
        public UI_ShopBuffBtn m_click;
        public Transition m_icon;
        public const string URL = "ui://ay4imj6tq4m88";

        public static UI_BuffShopItem CreateInstance()
        {
            return (UI_BuffShopItem)UIPackage.CreateObject("BuffShop", "BuffShopItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_time_bg = (GImage)GetChildAt(2);
            m_time = (GTextField)GetChildAt(3);
            m_value = (GTextField)GetChildAt(4);
            m_des = (GTextField)GetChildAt(5);
            m_click = (UI_ShopBuffBtn)GetChildAt(6);
            m_icon = GetTransitionAt(0);
        }
    }
}