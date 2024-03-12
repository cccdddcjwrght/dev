/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Guide
{
    public partial class UI_GuideFingerUI : GComponent
    {
        public UI_Finger m_Finger;
        public const string URL = "ui://hebbif0xk4sr4";

        public static UI_GuideFingerUI CreateInstance()
        {
            return (UI_GuideFingerUI)UIPackage.CreateObject("Guide", "GuideFingerUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_Finger = (UI_Finger)GetChildAt(0);
        }
    }
}