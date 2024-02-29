/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Shop
{
    public partial class UI_BigGoods : GLabel
    {
        public Controller m_type;
        public Controller m_saled;
        public GButton m_click;
        public GLabel m_left;
        public GList m_items;
        public GTextField m_desc;
        public GTextField m_count;
        public const string URL = "ui://aphwhwgnlxyll";

        public static UI_BigGoods CreateInstance()
        {
            return (UI_BigGoods)UIPackage.CreateObject("Shop", "BigGoods");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_type = GetControllerAt(0);
            m_saled = GetControllerAt(1);
            m_click = (GButton)GetChildAt(1);
            m_left = (GLabel)GetChildAt(2);
            m_items = (GList)GetChildAt(3);
            m_desc = (GTextField)GetChildAt(5);
            m_count = (GTextField)GetChildAt(6);
        }
    }
}