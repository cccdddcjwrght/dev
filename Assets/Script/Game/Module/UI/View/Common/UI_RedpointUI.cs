/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Common
{
    public partial class UI_RedpointUI : GComponent
    {
        public UI_Redpoint m_icon;
        public const string URL = "ui://2w8thcm7k6223lby";

        public static UI_RedpointUI CreateInstance()
        {
            return (UI_RedpointUI)UIPackage.CreateObject("Common", "RedpointUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_icon = (UI_Redpoint)GetChildAt(0);
        }
    }
}