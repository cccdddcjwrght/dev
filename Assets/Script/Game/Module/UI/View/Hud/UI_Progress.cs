/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Hud
{
    public partial class UI_Progress : GComponent
    {
        public GImage m_n0;
        public GImage m_n1;
        public const string URL = "ui://clbwsjawlrli6";

        public static UI_Progress CreateInstance()
        {
            return (UI_Progress)UIPackage.CreateObject("Hud", "Progress");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_n0 = (GImage)GetChildAt(0);
            m_n1 = (GImage)GetChildAt(1);
        }
    }
}