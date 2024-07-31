/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Task
{
    public partial class UI_TaskUI : GComponent
    {
        public GGraph m_mask;
        public GLabel m_body;
        public GTextField m_des;
        public GLoader m_icon;
        public UI_TaskProgress m_progress;
        public GList m_list;
        public GButton m_btn;
        public GComponent m_gifts;
        public GGroup m_content;
        public const string URL = "ui://j3n85nt9uszl3";

        public static UI_TaskUI CreateInstance()
        {
            return (UI_TaskUI)UIPackage.CreateObject("Task", "TaskUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_mask = (GGraph)GetChildAt(0);
            m_body = (GLabel)GetChildAt(1);
            m_des = (GTextField)GetChildAt(3);
            m_icon = (GLoader)GetChildAt(5);
            m_progress = (UI_TaskProgress)GetChildAt(6);
            m_list = (GList)GetChildAt(8);
            m_btn = (GButton)GetChildAt(9);
            m_gifts = (GComponent)GetChildAt(11);
            m_content = (GGroup)GetChildAt(12);
        }
    }
}