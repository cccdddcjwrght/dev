/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Shop
{
    public partial class UI_EqItem : GLabel
    {
        public Controller m_color;
        public const string URL = "ui://aphwhwgnlxylw";

        public static UI_EqItem CreateInstance()
        {
            return (UI_EqItem)UIPackage.CreateObject("Shop", "EqItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_color = GetControllerAt(0);
        }
    }
}