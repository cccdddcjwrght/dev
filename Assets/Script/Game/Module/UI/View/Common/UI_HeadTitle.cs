/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Common
{
    public partial class UI_HeadTitle : GLabel
    {
        public Controller m_size;
        public Controller m_timestate;
        public Controller m_closetype;
        public GRichTextField m_time;
        public UI_CloseBtn m_close;
        public const string URL = "ui://2w8thcm7e9cj14";

        public static UI_HeadTitle CreateInstance()
        {
            return (UI_HeadTitle)UIPackage.CreateObject("Common", "HeadTitle");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_size = GetControllerAt(0);
            m_timestate = GetControllerAt(1);
            m_closetype = GetControllerAt(2);
            m_time = (GRichTextField)GetChildAt(1);
            m_close = (UI_CloseBtn)GetChildAt(6);
        }
    }
}