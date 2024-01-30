/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Worktable
{
    public partial class UI_Panel : GLabel
    {
        public Controller m_type;
        public GComponent m_bg;
        public GTextField m_level;
        public GList m_list;
        public GProgressBar m_progress;
        public GLoader m_reward;
        public GLabel m_time;
        public GLabel m_price;
        public GButton m_click;
        public GList m_tips;
        public const string URL = "ui://m8rpv7f9b32eb";

        public static UI_Panel CreateInstance()
        {
            return (UI_Panel)UIPackage.CreateObject("Worktable", "Panel");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_type = GetControllerAt(0);
            m_bg = (GComponent)GetChildAt(0);
            m_level = (GTextField)GetChildAt(1);
            m_list = (GList)GetChildAt(3);
            m_progress = (GProgressBar)GetChildAt(4);
            m_reward = (GLoader)GetChildAt(5);
            m_time = (GLabel)GetChildAt(6);
            m_price = (GLabel)GetChildAt(7);
            m_click = (GButton)GetChildAt(9);
            m_tips = (GList)GetChildAt(10);
        }
    }
}