/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Task
{
    public partial class UI_TaskRewardItem : GComponent
    {
        public GLoader m_icon;
        public GTextField m___text;
        public const string URL = "ui://j3n85nt9uszl4";

        public static UI_TaskRewardItem CreateInstance()
        {
            return (UI_TaskRewardItem)UIPackage.CreateObject("Task", "TaskRewardItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_icon = (GLoader)GetChildAt(1);
            m___text = (GTextField)GetChildAt(2);
        }
    }
}