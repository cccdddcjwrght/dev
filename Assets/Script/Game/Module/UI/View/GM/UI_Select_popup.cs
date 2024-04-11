/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.GM
{
    public partial class UI_Select_popup : GComponent
    {
        public GList m_list;
        public const string URL = "ui://wdfxjeelutmx5";

        public static UI_Select_popup CreateInstance()
        {
            return (UI_Select_popup)UIPackage.CreateObject("GM", "Select_popup");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_list = (GList)GetChildAt(1);
        }
    }
}