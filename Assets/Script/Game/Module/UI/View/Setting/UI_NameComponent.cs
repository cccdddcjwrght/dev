/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Setting
{
    public partial class UI_NameComponent : GButton
    {
        public Controller m_c1;
        public GLoader m_icon;
        public GTextField m___title;
        public const string URL = "ui://dpgt0e2zn1bm1c";

        public static UI_NameComponent CreateInstance()
        {
            return (UI_NameComponent)UIPackage.CreateObject("Setting", "NameComponent");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_c1 = GetControllerAt(0);
            m_icon = (GLoader)GetChildAt(0);
            m___title = (GTextField)GetChildAt(1);
        }
    }
}