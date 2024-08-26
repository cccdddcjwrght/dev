/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Hud
{
    public partial class UI_BattleText : GComponent
    {
        public GTextField m_title;
        public Transition m_float;
        public const string URL = "ui://clbwsjawm3fdh";

        public static UI_BattleText CreateInstance()
        {
            return (UI_BattleText)UIPackage.CreateObject("Hud", "BattleText");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_title = (GTextField)GetChildAt(0);
            m_float = GetTransitionAt(0);
        }
    }
}