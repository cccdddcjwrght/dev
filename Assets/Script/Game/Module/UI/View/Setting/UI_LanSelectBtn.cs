/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Setting
{
    public partial class UI_LanSelectBtn : GButton
    {
        public Controller m_button;
        public Controller m_language;
        public GImage m_n0;
        public GImage m_n1;
        public GImage m_n2;
        public GLoader m_icon;
        public const string URL = "ui://dpgt0e2zfpxr1d";

        public static UI_LanSelectBtn CreateInstance()
        {
            return (UI_LanSelectBtn)UIPackage.CreateObject("Setting", "LanSelectBtn");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_button = GetControllerAt(0);
            m_language = GetControllerAt(1);
            m_n0 = (GImage)GetChildAt(0);
            m_n1 = (GImage)GetChildAt(1);
            m_n2 = (GImage)GetChildAt(2);
            m_icon = (GLoader)GetChildAt(3);
        }
    }
}