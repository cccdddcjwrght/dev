/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.MonsterHunter
{
    public partial class UI_HunterRewardTips : GComponent
    {
        public Controller m_open;
        public Controller m_completed;
        public Controller m_type;
        public GList m_list;
        public Transition m_t0;
        public Transition m_t1;
        public Transition m_reset;
        public const string URL = "ui://my7wqjw6dfdxlbc";

        public static UI_HunterRewardTips CreateInstance()
        {
            return (UI_HunterRewardTips)UIPackage.CreateObject("MonsterHunter", "HunterRewardTips");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_open = GetControllerAt(0);
            m_completed = GetControllerAt(1);
            m_type = GetControllerAt(2);
            m_list = (GList)GetChildAt(2);
            m_t0 = GetTransitionAt(0);
            m_t1 = GetTransitionAt(1);
            m_reset = GetTransitionAt(2);
        }
    }
}