/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.MonsterHunter
{
    public partial class UI_HunterWheel : GComponent
    {
        public Controller m_auto;
        public UI_SelectPanel m_panel;
        public UI_HunterBtn m_playbtn;
        public GButton m_currency;
        public GButton m_power;
        public const string URL = "ui://my7wqjw6u2kdlbf";

        public static UI_HunterWheel CreateInstance()
        {
            return (UI_HunterWheel)UIPackage.CreateObject("MonsterHunter", "HunterWheel");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_auto = GetControllerAt(0);
            m_panel = (UI_SelectPanel)GetChildAt(0);
            m_playbtn = (UI_HunterBtn)GetChildAt(1);
            m_currency = (GButton)GetChildAt(3);
            m_power = (GButton)GetChildAt(4);
        }
    }
}