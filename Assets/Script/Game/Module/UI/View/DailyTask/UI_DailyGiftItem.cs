/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.DailyTask
{
    public partial class UI_DailyGiftItem : GLabel
    {
        public Controller m_state;
        public const string URL = "ui://ti6n81b9diea7";

        public static UI_DailyGiftItem CreateInstance()
        {
            return (UI_DailyGiftItem)UIPackage.CreateObject("DailyTask", "DailyGiftItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_state = GetControllerAt(0);
        }
    }
}