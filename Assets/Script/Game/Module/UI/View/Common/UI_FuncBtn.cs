/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Common
{
    public partial class UI_FuncBtn : GButton
    {
        public Controller m_iconShow;
        public Controller m___redpoint;
        public GImage m_redPoint;
        public GTextField m_time;
        public const string URL = "ui://2w8thcm7k0s63lb2";

        public static UI_FuncBtn CreateInstance()
        {
            return (UI_FuncBtn)UIPackage.CreateObject("Common", "FuncBtn");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_iconShow = GetControllerAt(1);
            m___redpoint = GetControllerAt(2);
            m_redPoint = (GImage)GetChildAt(2);
            m_time = (GTextField)GetChildAt(3);
        }
    }
}