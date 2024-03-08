/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Offline
{
    public partial class UI_TimeProgressSlider : GLabel
    {
        public GImage m_bar;
        public const string URL = "ui://cvd693pfk6227";

        public static UI_TimeProgressSlider CreateInstance()
        {
            return (UI_TimeProgressSlider)UIPackage.CreateObject("Offline", "TimeProgressSlider");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_bar = (GImage)GetChildAt(0);
        }
    }
}