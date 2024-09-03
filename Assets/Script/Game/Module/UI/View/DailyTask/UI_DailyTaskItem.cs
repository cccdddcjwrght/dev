/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.DailyTask
{
    public partial class UI_DailyTaskItem : GLabel
    {
        public Controller m_state;
        public GTextField m_value;
        public GTextField m_des;
        public GProgressBar m_progress;
        public GButton m_goBtn;
        public GButton m_getBtn;
        public const string URL = "ui://ti6n81b9diea8";

        public static UI_DailyTaskItem CreateInstance()
        {
            return (UI_DailyTaskItem)UIPackage.CreateObject("DailyTask", "DailyTaskItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_state = GetControllerAt(0);
            m_value = (GTextField)GetChildAt(3);
            m_des = (GTextField)GetChildAt(4);
            m_progress = (GProgressBar)GetChildAt(5);
            m_goBtn = (GButton)GetChildAt(6);
            m_getBtn = (GButton)GetChildAt(7);
        }
    }
}