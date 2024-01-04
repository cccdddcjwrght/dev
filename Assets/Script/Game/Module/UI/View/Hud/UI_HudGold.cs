/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Hud
{
    public partial class UI_HudGold : GComponent
    {
        public GComponent m_gold;
        public const string URL = "ui://clbwsjawf7ke2";

        public static UI_HudGold CreateInstance()
        {
            return (UI_HudGold)UIPackage.CreateObject("Hud", "HudGold");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_gold = (GComponent)GetChildAt(0);
        }
    }
}