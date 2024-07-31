/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Worktable
{
    public partial class UI_RecBody : GComponent
    {
        public Controller m_roletype;
        public Controller m_recommand;
        public Controller m_selectctr;
        public UI_RecWorkerItem m_select1;
        public UI_RecWorkerItem m_select2;
        public const string URL = "ui://m8rpv7f9zsf41k";

        public static UI_RecBody CreateInstance()
        {
            return (UI_RecBody)UIPackage.CreateObject("Worktable", "RecBody");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_roletype = GetControllerAt(0);
            m_recommand = GetControllerAt(1);
            m_selectctr = GetControllerAt(2);
            m_select1 = (UI_RecWorkerItem)GetChildAt(0);
            m_select2 = (UI_RecWorkerItem)GetChildAt(1);
        }
    }
}