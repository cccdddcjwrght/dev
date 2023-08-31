/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Travel
{
    public partial class UI_Gold : GProgressBar
    {
        public Controller m_addhide;
        public Controller m_size;
        public Controller m_mode;
        public Controller m_titlemode;
        public GLoader m_icon;
        public GTextField m_shadow;
        public Transition m_reward;
        public const string URL = "ui://mssp6qbapp0xb";

        public static UI_Gold CreateInstance()
        {
            return (UI_Gold)UIPackage.CreateObject("Travel", "Gold");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_addhide = GetControllerAt(0);
            m_size = GetControllerAt(1);
            m_mode = GetControllerAt(2);
            m_titlemode = GetControllerAt(3);
            m_icon = (GLoader)GetChildAt(2);
            m_shadow = (GTextField)GetChildAt(3);
            m_reward = GetTransitionAt(0);
        }
    }
}