/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Common
{
    public partial class UI_ItemIcon : GLabel
    {
        public Controller m_icon;
        public Controller m_textsize;
        public Controller m_textpos;
        public const string URL = "ui://2w8thcm7vujj3lbg";

        public static UI_ItemIcon CreateInstance()
        {
            return (UI_ItemIcon)UIPackage.CreateObject("Common", "ItemIcon");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_icon = GetControllerAt(0);
            m_textsize = GetControllerAt(1);
            m_textpos = GetControllerAt(2);
        }
    }
}