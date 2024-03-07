/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Offline
{
    public partial class UI_TimePorgress : GProgressBar
    {
        public GTextField m___text;
        public const string URL = "ui://cvd693pfk6226";

        public static UI_TimePorgress CreateInstance()
        {
            return (UI_TimePorgress)UIPackage.CreateObject("Offline", "TimePorgress");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m___text = (GTextField)GetChildAt(3);
        }
    }
}