/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.BuffShop
{
    public partial class UI_shopclick : GButton
    {
        public Controller m_currency;
        public const string URL = "ui://ay4imj6tpvwq9";

        public static UI_shopclick CreateInstance()
        {
            return (UI_shopclick)UIPackage.CreateObject("BuffShop", "shopclick");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_currency = GetControllerAt(0);
        }
    }
}