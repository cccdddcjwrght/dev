/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Cookbook
{
    public partial class UI_WorkerItem : GComponent
    {
        public Controller m_selected;
        public Controller m_lock;
        public Controller m_state;
        public Controller m_maxlv;
        public Controller m___redpoint;
        public UI_Customer m_role;
        public GList m_stars;
        public UI_WorkerProgress m_progress;
        public const string URL = "ui://n2tgmsyuadfx2m";

        public static UI_WorkerItem CreateInstance()
        {
            return (UI_WorkerItem)UIPackage.CreateObject("Cookbook", "WorkerItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_selected = GetControllerAt(0);
            m_lock = GetControllerAt(1);
            m_state = GetControllerAt(2);
            m_maxlv = GetControllerAt(3);
            m___redpoint = GetControllerAt(4);
            m_role = (UI_Customer)GetChildAt(3);
            m_stars = (GList)GetChildAt(4);
            m_progress = (UI_WorkerProgress)GetChildAt(5);
        }
    }
}