/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Common
{
    public partial class UI_ColorText : GComponent
    {
        public Controller m_color;
        public Controller m_fontSize;
        public GRichTextField m_title_4;
        public GRichTextField m_title_0;
        public GRichTextField m_title_1;
        public GRichTextField m_title_2;
        public GRichTextField m_title_3;
        public const string URL = "ui://2w8thcm7s2fxak";

        public static UI_ColorText CreateInstance()
        {
            return (UI_ColorText)UIPackage.CreateObject("Common", "ColorText");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_color = GetControllerAt(0);
            m_fontSize = GetControllerAt(1);
            m_title_4 = (GRichTextField)GetChildAt(0);
            m_title_0 = (GRichTextField)GetChildAt(1);
            m_title_1 = (GRichTextField)GetChildAt(2);
            m_title_2 = (GRichTextField)GetChildAt(3);
            m_title_3 = (GRichTextField)GetChildAt(4);
        }
    }
}