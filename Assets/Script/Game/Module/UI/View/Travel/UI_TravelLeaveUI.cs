/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Travel
{
    public partial class UI_TravelLeaveUI : GComponent
    {
        public UI_Head m_head;
        public UI_Gold m_gold;
        public const string URL = "ui://mssp6qbapp0xa";

        public static UI_TravelLeaveUI CreateInstance()
        {
            return (UI_TravelLeaveUI)UIPackage.CreateObject("Travel", "TravelLeaveUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_head = (UI_Head)GetChildAt(1);
            m_gold = (UI_Gold)GetChildAt(2);
        }
    }
}