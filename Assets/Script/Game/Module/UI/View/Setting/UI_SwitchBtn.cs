/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Setting
{
    public partial class UI_SwitchBtn : GButton
    {
        public Controller m_button;
        public Controller m___disablelocal;
        public GImage m_n1;
        public GImage m_n8;
        public GGroup m_n5;
        public GImage m_n0;
        public const string URL = "ui://dpgt0e2zn1bmv";

        public static UI_SwitchBtn CreateInstance()
        {
            return (UI_SwitchBtn)UIPackage.CreateObject("Setting", "SwitchBtn");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_button = GetControllerAt(0);
            m___disablelocal = GetControllerAt(1);
            m_n1 = (GImage)GetChildAt(0);
            m_n8 = (GImage)GetChildAt(1);
            m_n5 = (GGroup)GetChildAt(2);
            m_n0 = (GImage)GetChildAt(3);
        }
    }
}