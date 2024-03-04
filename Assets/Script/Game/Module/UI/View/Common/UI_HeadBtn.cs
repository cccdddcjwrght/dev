/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Common
{
    public partial class UI_HeadBtn : GButton
    {
        public Controller m_redPointShow;
        public Controller m_state;
        public GImage m_redPoint;
        public GLoader m_frame;
        public GLoader m_headImg;
        public const string URL = "ui://2w8thcm7k0s63lb4";

        public static UI_HeadBtn CreateInstance()
        {
            return (UI_HeadBtn)UIPackage.CreateObject("Common", "HeadBtn");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_redPointShow = GetControllerAt(0);
            m_state = GetControllerAt(1);
            m_redPoint = (GImage)GetChildAt(0);
            m_frame = (GLoader)GetChildAt(1);
            m_headImg = (GLoader)GetChildAt(2);
        }
    }
}