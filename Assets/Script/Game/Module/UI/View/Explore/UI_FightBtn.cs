/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Explore
{
    public partial class UI_FightBtn : GButton
    {
        public Controller m_state;
        public GImage m_bar;
        public GTextField m_fight;
        public GTextField m_time;
        public Transition m_t0;
        public const string URL = "ui://ow12is1hkp2b25";

        public static UI_FightBtn CreateInstance()
        {
            return (UI_FightBtn)UIPackage.CreateObject("Explore", "FightBtn");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_state = GetControllerAt(1);
            m_bar = (GImage)GetChildAt(1);
            m_fight = (GTextField)GetChildAt(4);
            m_time = (GTextField)GetChildAt(6);
            m_t0 = GetTransitionAt(0);
        }
    }
}