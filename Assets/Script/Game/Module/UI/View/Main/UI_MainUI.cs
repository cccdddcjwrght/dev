/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Main
{
    public partial class UI_MainUI : GComponent
    {
        public Controller m_main;
        public GLoader m_top;
        public UI_ActBtnList m_rightList;
        public UI_ShowBtnList m_leftList;
        public GButton m_head;
        public GButton m_Gold;
        public GButton m_Diamond;
        public UI_BuffBtn m_buff;
        public UI_LikeBtn m_likeBtn;
        public UI_TotalBtn m_totalBtn;
        public GButton m_levelBtn;
        public GButton m_taskRewardBtn;
        public GButton m_AdBtn;
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
            m_top = (GLoader)GetChildAt(0);
            m_rightList = (UI_ActBtnList)GetChildAt(1);
            m_leftList = (UI_ShowBtnList)GetChildAt(2);
            m_head = (GButton)GetChildAt(3);
            m_Gold = (GButton)GetChildAt(4);
            m_Diamond = (GButton)GetChildAt(5);
            m_buff = (UI_BuffBtn)GetChildAt(6);
            m_likeBtn = (UI_LikeBtn)GetChildAt(7);
            m_totalBtn = (UI_TotalBtn)GetChildAt(8);
            m_levelBtn = (GButton)GetChildAt(9);
            m_taskRewardBtn = (GButton)GetChildAt(10);
            m_AdBtn = (GButton)GetChildAt(11);
            m_doshow = GetTransitionAt(0);
            m_dohide = GetTransitionAt(1);
        }
    }
}