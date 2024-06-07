/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Cookbook
{
    public partial class UI_CookbookUpUI : GLabel
    {
        public Controller m_type;
        public Controller m_currency;
        public GLabel m_body;
        public GTextField m_tips;
        public GTextField m_level;
        public GList m_stars;
        public GList m_pros;
        public GButton m_click;
        public GTextField m_limit;
        public GTextField m_cost;
        public Transition m_t0;
        public const string URL = "ui://n2tgmsyur4i19";

        public static UI_CookbookUpUI CreateInstance()
        {
            return (UI_CookbookUpUI)UIPackage.CreateObject("Cookbook", "CookbookUpUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_type = GetControllerAt(0);
            m_currency = GetControllerAt(1);
            m_body = (GLabel)GetChildAt(0);
            m_tips = (GTextField)GetChildAt(6);
            m_level = (GTextField)GetChildAt(7);
            m_stars = (GList)GetChildAt(9);
            m_pros = (GList)GetChildAt(10);
            m_click = (GButton)GetChildAt(11);
            m_limit = (GTextField)GetChildAt(12);
            m_cost = (GTextField)GetChildAt(13);
            m_t0 = GetTransitionAt(0);
        }
    }
}