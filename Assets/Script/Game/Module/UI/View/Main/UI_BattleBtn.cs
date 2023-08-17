/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Main
{
    public partial class UI_BattleBtn : GLabel
    {
        public Controller m_button;
        public UI_DiceBtn m_main;
        public GButton m_power;
        public GProgressBar m_countprogress;
        public GTextField m_maxtext;
        public GTextField m_time;
        public Transition m_t0;
        public const string URL = "ui://ktixaqljgmj1u";

        public static UI_BattleBtn CreateInstance()
        {
            return (UI_BattleBtn)UIPackage.CreateObject("Main", "BattleBtn");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_button = GetControllerAt(0);
            m_main = (UI_DiceBtn)GetChildAt(3);
            m_power = (GButton)GetChildAt(4);
            m_countprogress = (GProgressBar)GetChildAt(5);
            m_maxtext = (GTextField)GetChildAt(6);
            m_time = (GTextField)GetChildAt(7);
            m_t0 = GetTransitionAt(0);
        }
    }
}