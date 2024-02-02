/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Common
{
    public partial class UI_Currency : GButton
    {
        public Controller m_iconShow;
        public Controller m_AddShow;
        public GImage m_bg;
        public GLoader m_icon;
        public GTextField m___text;
        public GImage m_Add;
        public const string URL = "ui://2w8thcm7k0s63lb6";

        public static UI_Currency CreateInstance()
        {
            return (UI_Currency)UIPackage.CreateObject("Common", "Currency");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_iconShow = GetControllerAt(0);
            m_AddShow = GetControllerAt(1);
            m_bg = (GImage)GetChildAt(0);
            m_icon = (GLoader)GetChildAt(1);
            m___text = (GTextField)GetChildAt(2);
            m_Add = (GImage)GetChildAt(3);
        }
    }
}