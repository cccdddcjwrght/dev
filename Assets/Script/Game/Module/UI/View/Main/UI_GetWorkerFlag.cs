/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Main
{
    public partial class UI_GetWorkerFlag : GButton
    {
        public Controller m_type;
        public GComponent m_bg;
        public const string URL = "ui://ktixaqljddqnlbi";

        public static UI_GetWorkerFlag CreateInstance()
        {
            return (UI_GetWorkerFlag)UIPackage.CreateObject("Main", "GetWorkerFlag");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_type = GetControllerAt(0);
            m_bg = (GComponent)GetChildAt(0);
        }
    }
}