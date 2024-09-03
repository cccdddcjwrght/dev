/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.DailyTask
{
    public partial class UI_DailyRewardItem : GLabel
    {
        public Controller m_state;
        public GTextField m_value;
        public GTextField m_num;
        public const string URL = "ui://ti6n81b9dieac";

        public static UI_DailyRewardItem CreateInstance()
        {
            return (UI_DailyRewardItem)UIPackage.CreateObject("DailyTask", "DailyRewardItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_state = GetControllerAt(0);
            m_value = (GTextField)GetChildAt(2);
            m_num = (GTextField)GetChildAt(5);
        }
    }
}