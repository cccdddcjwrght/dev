/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Travel
{
    public partial class UI_Currency : GProgressBar
    {
        public Controller m_addhide;
        public Controller m_size;
        public Controller m_mode;
        public Controller m_titlemode;
        public GLoader m_icon;
        public GTextField m_shadow;
        public GTextField m_lv;
        public Transition m_reward;
        public const string URL = "ui://mssp6qbapp0x3lac";

        public static UI_Currency CreateInstance()
        {
            return (UI_Currency)UIPackage.CreateObject("Travel", "Currency");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_addhide = GetControllerAt(0);
            m_size = GetControllerAt(1);
            m_mode = GetControllerAt(2);
            m_titlemode = GetControllerAt(3);
            m_icon = (GLoader)GetChildAt(1);
            m_shadow = (GTextField)GetChildAt(2);
            m_lv = (GTextField)GetChildAt(4);
            m_reward = GetTransitionAt(0);
        }
    }
}