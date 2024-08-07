/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.EnterScene
{
    public partial class UI_goodItem : GLabel
    {
        public Controller m_isNew;
        public GTextField m_new;
        public const string URL = "ui://cxpm3jfbtem840";

        public static UI_goodItem CreateInstance()
        {
            return (UI_goodItem)UIPackage.CreateObject("EnterScene", "goodItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_isNew = GetControllerAt(0);
            m_new = (GTextField)GetChildAt(2);
        }
    }
}