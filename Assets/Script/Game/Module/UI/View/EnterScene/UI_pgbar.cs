/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.EnterScene
{
    public partial class UI_pgbar : GComponent
    {
        public GImage m_bar;
        public const string URL = "ui://cxpm3jfbudvnz";

        public static UI_pgbar CreateInstance()
        {
            return (UI_pgbar)UIPackage.CreateObject("EnterScene", "pgbar");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_bar = (GImage)GetChildAt(0);
        }
    }
}