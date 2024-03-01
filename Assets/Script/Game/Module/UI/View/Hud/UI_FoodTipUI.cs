/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Hud
{
    public partial class UI_FoodTipUI : GComponent
    {
        public GTextField m_title;
        public GLoader m_icon;
        public const string URL = "ui://clbwsjawry6u9";

        public static UI_FoodTipUI CreateInstance()
        {
            return (UI_FoodTipUI)UIPackage.CreateObject("Hud", "FoodTipUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_title = (GTextField)GetChildAt(0);
            m_icon = (GLoader)GetChildAt(1);
        }
    }
}