/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Common
{
    public partial class UI_ConfirmBody : GLabel
    {
        public Controller m_type;
        public Controller m_clickstate;
        public Controller m_textsize;
        public UI_PopupUI m_body;
        public UI_ClickBtn m_ok;
        public UI_ClickBtn m_click1;
        public UI_ClickBtn m_click2;
        public GLoader m_context;
        public GLoader m_top;
        public GRichTextField m_tips;
        public GRichTextField m_text;
        public const string URL = "ui://2w8thcm7l7ed3ldx";

        public static UI_ConfirmBody CreateInstance()
        {
            return (UI_ConfirmBody)UIPackage.CreateObject("Common", "ConfirmBody");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_type = GetControllerAt(0);
            m_clickstate = GetControllerAt(1);
            m_textsize = GetControllerAt(2);
            m_body = (UI_PopupUI)GetChildAt(0);
            m_ok = (UI_ClickBtn)GetChildAt(1);
            m_click1 = (UI_ClickBtn)GetChildAt(2);
            m_click2 = (UI_ClickBtn)GetChildAt(3);
            m_context = (GLoader)GetChildAt(6);
            m_top = (GLoader)GetChildAt(7);
            m_tips = (GRichTextField)GetChildAt(8);
            m_text = (GRichTextField)GetChildAt(9);
        }
    }
}