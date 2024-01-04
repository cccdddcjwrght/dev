/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Common
{
    public partial class UI_AdBtn : GButton
    {
        public Controller m_iconShow;
        public Controller m_redPointShow;
        public GImage m_bg;
        public GImage m_redPoint;
        public GTextField m_boostTxt;
        public GTextField m_timeTxt;
        public const string URL = "ui://2w8thcm7k0s63lb3";

        public static UI_AdBtn CreateInstance()
        {
            return (UI_AdBtn)UIPackage.CreateObject("Common", "AdBtn");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_iconShow = GetControllerAt(1);
            m_redPointShow = GetControllerAt(2);
            m_bg = (GImage)GetChildAt(0);
            m_redPoint = (GImage)GetChildAt(2);
            m_boostTxt = (GTextField)GetChildAt(3);
            m_timeTxt = (GTextField)GetChildAt(4);
        }
    }
}