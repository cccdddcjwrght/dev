/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Hud
{
    public partial class UI_FloatText : GComponent
    {
        public GTextField m_title;
        public const string URL = "ui://clbwsjawjx7g1";

        public static UI_FloatText CreateInstance()
        {
            return (UI_FloatText)UIPackage.CreateObject("Hud", "FloatText");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_title = (GTextField)GetChildAt(0);
        }
    }
}