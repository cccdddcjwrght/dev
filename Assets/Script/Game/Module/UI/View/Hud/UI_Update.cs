/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Hud
{
    public partial class UI_Update : GComponent
    {
        public GImage m_n2;
        public const string URL = "ui://clbwsjawbw2d7";

        public static UI_Update CreateInstance()
        {
            return (UI_Update)UIPackage.CreateObject("Hud", "Update");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_n2 = (GImage)GetChildAt(0);
        }
    }
}