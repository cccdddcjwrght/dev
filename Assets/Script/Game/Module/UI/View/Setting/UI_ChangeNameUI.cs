/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Setting
{
    public partial class UI_ChangeNameUI : GComponent
    {
        public GLabel m_body;
        public GImage m_n4;
        public GTextInput m_input;
        public GTextField m_tips;
        public GButton m_btnOK;
        public GGroup m_n16;
        public const string URL = "ui://dpgt0e2zfpxr1e";

        public static UI_ChangeNameUI CreateInstance()
        {
            return (UI_ChangeNameUI)UIPackage.CreateObject("Setting", "ChangeNameUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_body = (GLabel)GetChildAt(0);
            m_n4 = (GImage)GetChildAt(1);
            m_input = (GTextInput)GetChildAt(2);
            m_tips = (GTextField)GetChildAt(3);
            m_btnOK = (GButton)GetChildAt(4);
            m_n16 = (GGroup)GetChildAt(5);
        }
    }
}