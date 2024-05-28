/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Common
{
    public partial class UI_LockRedUI : GComponent
    {
        public Controller m_type;
        public Controller m_flag;
        public GLoader m_icon;
        public GLoader m_child;
        public Transition m_t0;
        public const string URL = "ui://2w8thcm7n5vf3lfx";

        public static UI_LockRedUI CreateInstance()
        {
            return (UI_LockRedUI)UIPackage.CreateObject("Common", "LockRedUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_type = GetControllerAt(0);
            m_flag = GetControllerAt(1);
            m_icon = (GLoader)GetChildAt(0);
            m_child = (GLoader)GetChildAt(1);
            m_t0 = GetTransitionAt(0);
        }
    }
}