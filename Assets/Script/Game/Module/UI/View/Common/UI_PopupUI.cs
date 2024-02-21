/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Common
{
    public partial class UI_PopupUI : GLabel
    {
        public Controller m_size;
        public GImage m_bg;
        public GTextField m_title;
        public UI_CloseBtn m_close;
        public GGraph m_closeBg;
        public const string URL = "ui://2w8thcm7k0s63lam";

        public static UI_PopupUI CreateInstance()
        {
            return (UI_PopupUI)UIPackage.CreateObject("Common", "PopupUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_size = GetControllerAt(0);
            m_bg = (GImage)GetChildAt(0);
            m_title = (GTextField)GetChildAt(1);
            m_close = (UI_CloseBtn)GetChildAt(2);
            m_closeBg = (GGraph)GetChildAt(3);
        }
    }
}