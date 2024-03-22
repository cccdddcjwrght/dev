/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Hud
{
    public partial class UI_Effect : GComponent
    {
        public GGraph m___effect;
        public const string URL = "ui://clbwsjawrlhtc";

        public static UI_Effect CreateInstance()
        {
            return (UI_Effect)UIPackage.CreateObject("Hud", "Effect");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m___effect = (GGraph)GetChildAt(0);
        }
    }
}