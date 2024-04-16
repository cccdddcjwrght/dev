/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Reputation
{
    public partial class UI_GoodReputationUI : GLabel
    {
        public Controller m_state;
        public GLabel m_body;
        public GLoader m_progress;
        public GTextField m_time;
        public GTextField m_info;
        public GList m_list;
        public GGroup m_group;
        public const string URL = "ui://vnok3a309ehb6";

        public static UI_GoodReputationUI CreateInstance()
        {
            return (UI_GoodReputationUI)UIPackage.CreateObject("Reputation", "GoodReputationUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_state = GetControllerAt(0);
            m_body = (GLabel)GetChildAt(0);
            m_progress = (GLoader)GetChildAt(2);
            m_time = (GTextField)GetChildAt(5);
            m_info = (GTextField)GetChildAt(7);
            m_list = (GList)GetChildAt(8);
            m_group = (GGroup)GetChildAt(9);
        }
    }
}