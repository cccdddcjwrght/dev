/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.DailyTask
{
    public partial class UI_DailyPop : GLabel
    {
        public GList m_list;
        public const string URL = "ui://ti6n81b9gtlpd";

        public static UI_DailyPop CreateInstance()
        {
            return (UI_DailyPop)UIPackage.CreateObject("DailyTask", "DailyPop");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_list = (GList)GetChildAt(1);
        }
    }
}