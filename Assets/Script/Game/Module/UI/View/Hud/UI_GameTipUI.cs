/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Hud
{
    public partial class UI_GameTipUI : GComponent
    {
        public Controller m_TipType;
        public GTextField m_title;
        public Transition m_float;
        public const string URL = "ui://clbwsjawiogmb";

        public static UI_GameTipUI CreateInstance()
        {
            return (UI_GameTipUI)UIPackage.CreateObject("Hud", "GameTipUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_TipType = GetControllerAt(0);
            m_title = (GTextField)GetChildAt(0);
            m_float = GetTransitionAt(0);
        }
    }
}