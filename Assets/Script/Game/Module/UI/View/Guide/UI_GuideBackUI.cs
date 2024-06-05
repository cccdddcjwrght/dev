/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Guide
{
    public partial class UI_GuideBackUI : GComponent
    {
        public GGraph m_mask1;
        public GGraph m_mask2;
        public GGraph m_mask3;
        public GGraph m_mask4;
        public GGraph m_mask;
        public GGraph m_blank;
        public const string URL = "ui://hebbif0x91jv6";

        public static UI_GuideBackUI CreateInstance()
        {
            return (UI_GuideBackUI)UIPackage.CreateObject("Guide", "GuideBackUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_mask1 = (GGraph)GetChildAt(0);
            m_mask2 = (GGraph)GetChildAt(1);
            m_mask3 = (GGraph)GetChildAt(2);
            m_mask4 = (GGraph)GetChildAt(3);
            m_mask = (GGraph)GetChildAt(4);
            m_blank = (GGraph)GetChildAt(5);
        }
    }
}