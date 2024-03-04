/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Setting
{
    public partial class UI_NormalBtn : GButton
    {
        public Controller m_button;
        public Controller m_c1;
        public GImage m_n0;
        public GImage m_n2;
        public GTextField m_title;
        public const string URL = "ui://dpgt0e2zn1bm10";

        public static UI_NormalBtn CreateInstance()
        {
            return (UI_NormalBtn)UIPackage.CreateObject("Setting", "NormalBtn");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_button = GetControllerAt(0);
            m_c1 = GetControllerAt(1);
            m_n0 = (GImage)GetChildAt(0);
            m_n2 = (GImage)GetChildAt(1);
            m_title = (GTextField)GetChildAt(2);
        }
    }
}