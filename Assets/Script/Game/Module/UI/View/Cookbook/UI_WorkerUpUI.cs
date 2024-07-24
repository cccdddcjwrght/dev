/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Cookbook
{
    public partial class UI_WorkerUpUI : GLabel
    {
        public Controller m_state;
        public Controller m_selected;
        public Controller m_maxlv;
        public GLabel m_body;
        public UI_Customer m_customer;
        public GList m_stars;
        public GGraph m___effect;
        public GRichTextField m_desc;
        public UI_WorkerProgress m_progress;
        public GLoader m_matitem;
        public UI_WorkerAddProperty m_property;
        public GButton m_reward;
        public GButton m_click;
        public Transition m_t0;
        public const string URL = "ui://n2tgmsyuadfx2p";

        public static UI_WorkerUpUI CreateInstance()
        {
            return (UI_WorkerUpUI)UIPackage.CreateObject("Cookbook", "WorkerUpUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_state = GetControllerAt(0);
            m_selected = GetControllerAt(1);
            m_maxlv = GetControllerAt(2);
            m_body = (GLabel)GetChildAt(0);
            m_customer = (UI_Customer)GetChildAt(3);
            m_stars = (GList)GetChildAt(4);
            m___effect = (GGraph)GetChildAt(5);
            m_desc = (GRichTextField)GetChildAt(6);
            m_progress = (UI_WorkerProgress)GetChildAt(8);
            m_matitem = (GLoader)GetChildAt(9);
            m_property = (UI_WorkerAddProperty)GetChildAt(11);
            m_reward = (GButton)GetChildAt(12);
            m_click = (GButton)GetChildAt(13);
            m_t0 = GetTransitionAt(0);
        }
    }
}