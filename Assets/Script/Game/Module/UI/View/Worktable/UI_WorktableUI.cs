/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Worktable
{
    public partial class UI_WorktableUI : GLabel
    {
        public Controller m_type;
        public GGraph m_close;
        public const string URL = "ui://m8rpv7f9b32e1";

        public static UI_WorktableUI CreateInstance()
        {
            return (UI_WorktableUI)UIPackage.CreateObject("Worktable", "WorktableUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_type = GetControllerAt(0);
            m_close = (GGraph)GetChildAt(0);
        }
    }
}