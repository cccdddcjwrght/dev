/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Hud
{
    public partial class UI_Progress : GComponent
    {
        public GImage m_bg;
        public GImage m_progress;
        public const string URL = "ui://clbwsjawlrli6";

        public static UI_Progress CreateInstance()
        {
            return (UI_Progress)UIPackage.CreateObject("Hud", "Progress");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_bg = (GImage)GetChildAt(0);
            m_progress = (GImage)GetChildAt(1);
        }
    }
}