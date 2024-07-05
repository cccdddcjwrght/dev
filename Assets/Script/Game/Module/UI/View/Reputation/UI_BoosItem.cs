/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Reputation
{
    public partial class UI_BoosItem : GComponent
    {
        public GTextField m_name;
        public GTextField m_multiple;
        public GTextField m_duration;
        public const string URL = "ui://vnok3a30pynwc";

        public static UI_BoosItem CreateInstance()
        {
            return (UI_BoosItem)UIPackage.CreateObject("Reputation", "BoosItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_name = (GTextField)GetChildAt(0);
            m_multiple = (GTextField)GetChildAt(1);
            m_duration = (GTextField)GetChildAt(2);
        }
    }
}