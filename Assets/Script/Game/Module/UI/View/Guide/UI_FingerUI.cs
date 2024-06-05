/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Guide
{
    public partial class UI_FingerUI : GComponent
    {
        public UI_Finger m_Finger;
        public const string URL = "ui://hebbif0x91jv7";

        public static UI_FingerUI CreateInstance()
        {
            return (UI_FingerUI)UIPackage.CreateObject("Guide", "FingerUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_Finger = (UI_Finger)GetChildAt(0);
        }
    }
}