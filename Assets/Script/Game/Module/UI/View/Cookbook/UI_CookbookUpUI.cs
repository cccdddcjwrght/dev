/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Cookbook
{
    public partial class UI_CookbookUpUI : GLabel
    {
        public Controller m_type;
        public GLabel m_body;
        public GTextField m_tips;
        public GList m_stars;
        public GList m_pros;
        public GButton m_click;
        public GTextField m_limit;
        public GTextField m_cost;
        public const string URL = "ui://n2tgmsyur4i19";

        public static UI_CookbookUpUI CreateInstance()
        {
            return (UI_CookbookUpUI)UIPackage.CreateObject("Cookbook", "CookbookUpUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_type = GetControllerAt(0);
            m_body = (GLabel)GetChildAt(0);
            m_tips = (GTextField)GetChildAt(5);
            m_stars = (GList)GetChildAt(7);
            m_pros = (GList)GetChildAt(8);
            m_click = (GButton)GetChildAt(9);
            m_limit = (GTextField)GetChildAt(10);
            m_cost = (GTextField)GetChildAt(11);
        }
    }
}