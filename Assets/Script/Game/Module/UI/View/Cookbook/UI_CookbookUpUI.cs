/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Cookbook
{
    public partial class UI_CookbookUpUI : GLabel
    {
        public Controller m_type;
        public Controller m_mode;
        public GLabel m_body;
        public GGraph m___effect;
        public GList m_pros;
        public GTextField m_tips;
        public GTextField m_level;
        public GTextField m_limit;
        public GRichTextField m_cost;
        public GLoader m_item;
        public GButton m_click;
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
            m_mode = GetControllerAt(1);
            m_body = (GLabel)GetChildAt(0);
            m___effect = (GGraph)GetChildAt(7);
            m_pros = (GList)GetChildAt(8);
            m_tips = (GTextField)GetChildAt(11);
            m_level = (GTextField)GetChildAt(12);
            m_limit = (GTextField)GetChildAt(13);
            m_cost = (GRichTextField)GetChildAt(14);
            m_item = (GLoader)GetChildAt(15);
            m_click = (GButton)GetChildAt(18);
            m_t0 = GetTransitionAt(0);
        }
    }
}