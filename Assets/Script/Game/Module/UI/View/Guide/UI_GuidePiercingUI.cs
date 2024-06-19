/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Guide
{
    public partial class UI_GuidePiercingUI : GComponent
    {
        public GGraph m_mask;
        public GGraph m_blank;
        public const string URL = "ui://hebbif0xsibqc";

        public static UI_GuidePiercingUI CreateInstance()
        {
            return (UI_GuidePiercingUI)UIPackage.CreateObject("Guide", "GuidePiercingUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_mask = (GGraph)GetChildAt(0);
            m_blank = (GGraph)GetChildAt(1);
        }
    }
}