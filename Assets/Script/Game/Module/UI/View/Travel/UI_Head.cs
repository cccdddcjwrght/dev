/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Travel
{
    public partial class UI_Head : GComponent
    {
        public GLoader m_icon;
        public const string URL = "ui://mssp6qbapp0x8";

        public static UI_Head CreateInstance()
        {
            return (UI_Head)UIPackage.CreateObject("Travel", "Head");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_icon = (GLoader)GetChildAt(1);
        }
    }
}