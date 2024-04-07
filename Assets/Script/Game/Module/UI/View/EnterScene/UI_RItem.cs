/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.EnterScene
{
    public partial class UI_RItem : GButton
    {
        public GButton m_body;
        public const string URL = "ui://cxpm3jfbudvnv";

        public static UI_RItem CreateInstance()
        {
            return (UI_RItem)UIPackage.CreateObject("EnterScene", "RItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_body = (GButton)GetChildAt(0);
        }
    }
}