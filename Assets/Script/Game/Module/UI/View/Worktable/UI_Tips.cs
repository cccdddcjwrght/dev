/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Worktable
{
    public partial class UI_Tips : GLabel
    {
        public Controller m_type;
        public GTextField m_tips;
        public Transition m_t0;
        public const string URL = "ui://m8rpv7f9b32ea";

        public static UI_Tips CreateInstance()
        {
            return (UI_Tips)UIPackage.CreateObject("Worktable", "Tips");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_type = GetControllerAt(0);
            m_tips = (GTextField)GetChildAt(2);
            m_t0 = GetTransitionAt(0);
        }
    }
}