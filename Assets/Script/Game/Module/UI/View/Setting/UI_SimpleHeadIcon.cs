/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Setting
{
    public partial class UI_SimpleHeadIcon : GButton
    {
        public Controller m_lock;
        public Controller m_check;
        public Controller m_state;
        public GButton m_body;
        public GLoader m_check_2;
        public GImage m_lock_2;
        public Transition m_checkShow;
        public Transition m_checkHide;
        public const string URL = "ui://dpgt0e2zfpxr1h";

        public static UI_SimpleHeadIcon CreateInstance()
        {
            return (UI_SimpleHeadIcon)UIPackage.CreateObject("Setting", "SimpleHeadIcon");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_lock = GetControllerAt(1);
            m_check = GetControllerAt(2);
            m_state = GetControllerAt(3);
            m_body = (GButton)GetChildAt(0);
            m_check_2 = (GLoader)GetChildAt(1);
            m_lock_2 = (GImage)GetChildAt(2);
            m_checkShow = GetTransitionAt(0);
            m_checkHide = GetTransitionAt(1);
        }
    }
}