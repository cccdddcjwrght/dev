/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Travel
{
    public partial class UI_TravelEnterUI : GComponent
    {
        public UI_Currency m_currency;
        public GTextField m_name;
        public UI_Head m_head;
        public const string URL = "ui://mssp6qbapp0x7";

        public static UI_TravelEnterUI CreateInstance()
        {
            return (UI_TravelEnterUI)UIPackage.CreateObject("Travel", "TravelEnterUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_currency = (UI_Currency)GetChildAt(2);
            m_name = (GTextField)GetChildAt(3);
            m_head = (UI_Head)GetChildAt(4);
        }
    }
}