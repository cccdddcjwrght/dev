/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Worktable
{
    public partial class UI_RecWorkerItem : GButton
    {
        public Controller m_type;
        public Controller m_recommand;
        public GTextField m_count;
        public const string URL = "ui://m8rpv7f9zsf41e";

        public static UI_RecWorkerItem CreateInstance()
        {
            return (UI_RecWorkerItem)UIPackage.CreateObject("Worktable", "RecWorkerItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_type = GetControllerAt(0);
            m_recommand = GetControllerAt(1);
            m_count = (GTextField)GetChildAt(6);
        }
    }
}