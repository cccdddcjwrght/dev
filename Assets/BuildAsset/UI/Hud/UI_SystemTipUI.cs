/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Hud
{
    public partial class UI_SystemTipUI : GComponent
    {
        public GTextField m_title;
        public Transition m_myfloat;
        public const string URL = "ui://clbwsjawiogma";

        public static UI_SystemTipUI CreateInstance()
        {
            return (UI_SystemTipUI)UIPackage.CreateObject("Hud", "SystemTipUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_title = (GTextField)GetChildAt(1);
            m_myfloat = GetTransitionAt(0);
        }
    }
}