/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Hud
{
    public partial class UI_HudUI : GComponent
    {
        public UI_FloatText m_rrr;
        public UI_Progress m_pgr;
        public UI_OrderTip m_tr;
        public const string URL = "ui://clbwsjawjx7g0";

        public static UI_HudUI CreateInstance()
        {
            return (UI_HudUI)UIPackage.CreateObject("Hud", "HudUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_rrr = (UI_FloatText)GetChildAt(0);
            m_pgr = (UI_Progress)GetChildAt(1);
            m_tr = (UI_OrderTip)GetChildAt(2);
        }
    }
}