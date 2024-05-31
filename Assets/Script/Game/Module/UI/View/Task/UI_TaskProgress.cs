/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Task
{
    public partial class UI_TaskProgress : GProgressBar
    {
        public GTextField m_value;
        public const string URL = "ui://j3n85nt9uszl5";

        public static UI_TaskProgress CreateInstance()
        {
            return (UI_TaskProgress)UIPackage.CreateObject("Task", "TaskProgress");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_value = (GTextField)GetChildAt(2);
        }
    }
}