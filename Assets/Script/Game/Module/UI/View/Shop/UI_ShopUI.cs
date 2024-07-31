/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Shop
{
    public partial class UI_ShopUI : GLabel
    {
        public Controller m_pages;
        public Controller m_rate;
        public GGraph m_top;
        public GLabel m_body;
        public UI_ShopBody m_content;
        public GLoader m_clickBtn;
        public UI_Probability m_rate_2;
        public const string URL = "ui://aphwhwgnlxyle";

        public static UI_ShopUI CreateInstance()
        {
            return (UI_ShopUI)UIPackage.CreateObject("Shop", "ShopUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_pages = GetControllerAt(0);
            m_rate = GetControllerAt(1);
            m_top = (GGraph)GetChildAt(0);
            m_body = (GLabel)GetChildAt(2);
            m_content = (UI_ShopBody)GetChildAt(3);
            m_clickBtn = (GLoader)GetChildAt(4);
            m_rate_2 = (UI_Probability)GetChildAt(5);
        }
    }
}