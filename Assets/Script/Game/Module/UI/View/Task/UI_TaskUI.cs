/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Task
{
    public partial class UI_TaskUI : GComponent
    {
        public GGraph m_mask;
        public GTextField m_title;
        public GTextField m_des;
        public GLoader m_icon;
        public UI_TaskProgress m_progress;
        public GList m_list;
        public GButton m_btn;
        public GGroup m_content;
        public const string URL = "ui://j3n85nt9uszl3";

        public static UI_TaskUI CreateInstance()
        {
            return (UI_TaskUI)UIPackage.CreateObject("Task", "TaskUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_mask = (GGraph)GetChildAt(1);
            m_title = (GTextField)GetChildAt(4);
            m_des = (GTextField)GetChildAt(5);
            m_icon = (GLoader)GetChildAt(6);
            m_progress = (UI_TaskProgress)GetChildAt(7);
            m_list = (GList)GetChildAt(9);
            m_btn = (GButton)GetChildAt(10);
            m_content = (GGroup)GetChildAt(11);
        }
    }
}