/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Common
{
    public partial class UI_FuncBtn : GButton
    {
        public Controller m_button;
        public Controller m_iconShow;
        public Controller m_redPointShow;
        public GImage m_bg;
        public GLoader m_icon;
        public GImage m_redPoint;
        public const string URL = "ui://2w8thcm7k0s63lb2";

        public static UI_FuncBtn CreateInstance()
        {
            return (UI_FuncBtn)UIPackage.CreateObject("Common", "FuncBtn");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_button = GetControllerAt(0);
            m_iconShow = GetControllerAt(1);
            m_redPointShow = GetControllerAt(2);
            m_bg = (GImage)GetChildAt(0);
            m_icon = (GLoader)GetChildAt(1);
            m_redPoint = (GImage)GetChildAt(2);
        }
    }
}