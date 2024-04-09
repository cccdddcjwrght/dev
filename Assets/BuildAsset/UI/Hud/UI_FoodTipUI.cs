/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Hud
{
    public partial class UI_FoodTipUI : GComponent
    {
        public GTextField m_title;
        public GLoader m_icon;
        public GGroup m_content;
        public Transition m_t0;
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
            m_content = (GGroup)GetChildAt(2);
            m_t0 = GetTransitionAt(0);
        }
    }
}