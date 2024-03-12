/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Guide
{
    public partial class UI_GuideMaskUI : GComponent
    {
        public GGraph m_n0;
        public const string URL = "ui://hebbif0xcdxn5";

        public static UI_GuideMaskUI CreateInstance()
        {
            return (UI_GuideMaskUI)UIPackage.CreateObject("Guide", "GuideMaskUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_n0 = (GGraph)GetChildAt(0);
        }
    }
}