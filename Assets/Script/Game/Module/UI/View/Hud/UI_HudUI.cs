/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Hud
{
    public partial class UI_HudUI : GComponent
    {
        public UI_FloatText m_nn;
        public const string URL = "ui://clbwsjawjx7g0";

        public static UI_HudUI CreateInstance()
        {
            return (UI_HudUI)UIPackage.CreateObject("Hud", "HudUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_nn = (UI_FloatText)GetChildAt(0);
        }
    }
}