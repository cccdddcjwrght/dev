/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Exchange
{
    public partial class UI_ExchangeProgress : GProgressBar
    {
        public GTextField m_value;
        public const string URL = "ui://d6hhikg5ee8je";

        public static UI_ExchangeProgress CreateInstance()
        {
            return (UI_ExchangeProgress)UIPackage.CreateObject("Exchange", "ExchangeProgress");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_value = (GTextField)GetChildAt(2);
        }
    }
}