/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Cookbook
{
    public partial class UI_WorkerAddProperty : GLabel
    {
        public Controller m_type;
        public Controller m_fullsize;
        public Controller m_color;
        public GTextField m_full;
        public GTextField m_next;
        public Transition m_t0;
        public const string URL = "ui://n2tgmsyuadfx2n";

        public static UI_WorkerAddProperty CreateInstance()
        {
            return (UI_WorkerAddProperty)UIPackage.CreateObject("Cookbook", "WorkerAddProperty");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_type = GetControllerAt(0);
            m_fullsize = GetControllerAt(1);
            m_color = GetControllerAt(2);
            m_full = (GTextField)GetChildAt(3);
            m_next = (GTextField)GetChildAt(4);
            m_t0 = GetTransitionAt(0);
        }
    }
}