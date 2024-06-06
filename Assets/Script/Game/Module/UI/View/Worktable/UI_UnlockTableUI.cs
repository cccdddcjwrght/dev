/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Worktable
{
    public partial class UI_UnlockTableUI : GLabel
    {
        public Controller m_currency;
        public Controller m_type;
        public GLabel m_body;
        public GTextField m_tips;
        public GButton m_click;
        public GTextField m_cost;
        public GTextField m_count;
        public const string URL = "ui://m8rpv7f9hx7uc";

        public static UI_UnlockTableUI CreateInstance()
        {
            return (UI_UnlockTableUI)UIPackage.CreateObject("Worktable", "UnlockTableUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_currency = GetControllerAt(0);
            m_type = GetControllerAt(1);
            m_body = (GLabel)GetChildAt(0);
            m_tips = (GTextField)GetChildAt(3);
            m_click = (GButton)GetChildAt(4);
            m_cost = (GTextField)GetChildAt(5);
            m_count = (GTextField)GetChildAt(9);
        }
    }
}