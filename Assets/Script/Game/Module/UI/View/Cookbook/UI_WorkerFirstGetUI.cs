/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Cookbook
{
    public partial class UI_WorkerFirstGetUI : GLabel
    {
        public Controller m_type;
        public GGraph m_mask;
        public GGraph m___effect;
        public GList m_stars;
        public UI_Customer m_customer;
        public UI_WorkerProgress m_progress;
        public UI_WorkerAddProperty m_property;
        public Transition m_t0;
        public const string URL = "ui://n2tgmsyuadfx2q";

        public static UI_WorkerFirstGetUI CreateInstance()
        {
            return (UI_WorkerFirstGetUI)UIPackage.CreateObject("Cookbook", "WorkerFirstGetUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_type = GetControllerAt(0);
            m_mask = (GGraph)GetChildAt(0);
            m___effect = (GGraph)GetChildAt(3);
            m_stars = (GList)GetChildAt(4);
            m_customer = (UI_Customer)GetChildAt(5);
            m_progress = (UI_WorkerProgress)GetChildAt(8);
            m_property = (UI_WorkerAddProperty)GetChildAt(9);
            m_t0 = GetTransitionAt(0);
        }
    }
}