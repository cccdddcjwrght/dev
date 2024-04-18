/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Common
{
    public partial class UI_PopupUI : GLabel
    {
        public Controller m_size;
        public Controller m_type;
        public Controller m_hideclose;
        public GGraph m_closeBg;
        public GLoader m_bg;
        public GButton m_close;
        public const string URL = "ui://2w8thcm7k0s63lam";

        public static UI_PopupUI CreateInstance()
        {
            return (UI_PopupUI)UIPackage.CreateObject("Common", "PopupUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_size = GetControllerAt(0);
            m_type = GetControllerAt(1);
            m_hideclose = GetControllerAt(2);
            m_closeBg = (GGraph)GetChildAt(0);
            m_bg = (GLoader)GetChildAt(1);
            m_close = (GButton)GetChildAt(5);
        }
    }
}