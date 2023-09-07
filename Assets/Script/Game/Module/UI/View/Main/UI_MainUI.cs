/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Main
{
    public partial class UI_MainUI : GComponent
    {
        public Controller m_main;
        public Controller m_travel;
        public GProgressBar m_level;
        public GProgressBar m_gold;
        public GProgressBar m_diamond;
        public GGroup m_bar;
        public UI_ImgBtn m_setting;
        public UI_BattleBtn m_battle;
        public GTextField m_goldFloating;
        public UI_Tip m_tip;
        public Transition m_doshow;
        public Transition m_dohide;
        public const string URL = "ui://ktixaqljgmj1l";

        public static UI_MainUI CreateInstance()
        {
            return (UI_MainUI)UIPackage.CreateObject("Main", "MainUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_main = GetControllerAt(0);
            m_travel = GetControllerAt(1);
            m_level = (GProgressBar)GetChildAt(0);
            m_gold = (GProgressBar)GetChildAt(1);
            m_diamond = (GProgressBar)GetChildAt(2);
            m_bar = (GGroup)GetChildAt(3);
            m_setting = (UI_ImgBtn)GetChildAt(4);
            m_battle = (UI_BattleBtn)GetChildAt(6);
            m_goldFloating = (GTextField)GetChildAt(7);
            m_tip = (UI_Tip)GetChildAt(8);
            m_doshow = GetTransitionAt(0);
            m_dohide = GetTransitionAt(1);
        }
    }
}