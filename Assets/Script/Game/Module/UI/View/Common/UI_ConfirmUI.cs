/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Common
{
    public partial class UI_ConfirmUI : GLabel
    {
        public Controller m_type;
        public Controller m_clickstate;
        public Controller m_textsize;
        public UI_ConfirmBody m_body;
        public const string URL = "ui://2w8thcm7l7ed3ldw";

        public static UI_ConfirmUI CreateInstance()
        {
            return (UI_ConfirmUI)UIPackage.CreateObject("Common", "ConfirmUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_type = GetControllerAt(0);
            m_clickstate = GetControllerAt(1);
            m_textsize = GetControllerAt(2);
            m_body = (UI_ConfirmBody)GetChildAt(0);
        }
    }
}