/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Offline
{
    public partial class UI_OfflineUI : GLabel
    {
        public Controller m_state;
        public GLabel m_body;
        public UI_TimePorgress m_progress;
        public GTextField m_count;
        public GButton m_click;
        public const string URL = "ui://cvd693pfk6225";

        public static UI_OfflineUI CreateInstance()
        {
            return (UI_OfflineUI)UIPackage.CreateObject("Offline", "OfflineUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_state = GetControllerAt(0);
            m_body = (GLabel)GetChildAt(0);
            m_progress = (UI_TimePorgress)GetChildAt(4);
            m_count = (GTextField)GetChildAt(6);
            m_click = (GButton)GetChildAt(8);
        }
    }
}