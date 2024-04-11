/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.PiggyBank
{
    public partial class UI_PiggyBankUI : GComponent
    {
        public Controller m_state;
        public Controller m_stage;
        public GGraph m_mask;
        public GLabel m_body;
        public GButton m_tipBtn;
        public GTextField m_tip;
        public GLoader m_icon;
        public UI_PiggyBankProgress m_progress;
        public GButton m_buyBtn;
        public GTextField m_time;
        public GGroup m_group;
        public const string URL = "ui://k2a58dz5a9b7p";

        public static UI_PiggyBankUI CreateInstance()
        {
            return (UI_PiggyBankUI)UIPackage.CreateObject("PiggyBank", "PiggyBankUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_state = GetControllerAt(0);
            m_stage = GetControllerAt(1);
            m_mask = (GGraph)GetChildAt(0);
            m_body = (GLabel)GetChildAt(1);
            m_tipBtn = (GButton)GetChildAt(2);
            m_tip = (GTextField)GetChildAt(3);
            m_icon = (GLoader)GetChildAt(5);
            m_progress = (UI_PiggyBankProgress)GetChildAt(8);
            m_buyBtn = (GButton)GetChildAt(9);
            m_time = (GTextField)GetChildAt(10);
            m_group = (GGroup)GetChildAt(12);
        }
    }
}