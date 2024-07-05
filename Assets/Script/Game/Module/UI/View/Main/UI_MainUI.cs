/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Main
{
    public partial class UI_MainUI : GComponent
    {
        public Controller m_main;
        public Controller m_ad;
        public Controller m_getworker;
        public GImage m_bgTop;
        public GImage m_bgBottom;
        public GLoader m_top;
        public UI_ActBtnList m_rightList;
        public UI_ActBtnList m_leftList;
        public GButton m_head;
        public GButton m_Gold;
        public GButton m_Diamond;
        public UI_ActBtn m_btnShop;
        public UI_BuffBtn m_buff;
        public UI_LikeBtn m_likeBtn;
        public UI_TotalBtn m_totalBtn;
        public GButton m_friendBtn;
        public GButton m_petBtn;
        public GButton m_AdBtn;
        public GButton m_equipBtn;
        public GButton m_taskBtn;
        public UI_InvestMan m_InvestBtn;
        public UI_GetWorkerFlag m_workflag;
        public UI_HotFoodBtn m_hotFoodBtn;
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
            m_ad = GetControllerAt(1);
            m_getworker = GetControllerAt(2);
            m_bgTop = (GImage)GetChildAt(0);
            m_bgBottom = (GImage)GetChildAt(1);
            m_top = (GLoader)GetChildAt(2);
            m_rightList = (UI_ActBtnList)GetChildAt(3);
            m_leftList = (UI_ActBtnList)GetChildAt(4);
            m_head = (GButton)GetChildAt(5);
            m_Gold = (GButton)GetChildAt(6);
            m_Diamond = (GButton)GetChildAt(7);
            m_btnShop = (UI_ActBtn)GetChildAt(8);
            m_buff = (UI_BuffBtn)GetChildAt(9);
            m_likeBtn = (UI_LikeBtn)GetChildAt(10);
            m_totalBtn = (UI_TotalBtn)GetChildAt(11);
            m_friendBtn = (GButton)GetChildAt(12);
            m_petBtn = (GButton)GetChildAt(13);
            m_AdBtn = (GButton)GetChildAt(14);
            m_equipBtn = (GButton)GetChildAt(15);
            m_taskBtn = (GButton)GetChildAt(16);
            m_InvestBtn = (UI_InvestMan)GetChildAt(18);
            m_workflag = (UI_GetWorkerFlag)GetChildAt(19);
            m_hotFoodBtn = (UI_HotFoodBtn)GetChildAt(20);
            m_doshow = GetTransitionAt(0);
            m_dohide = GetTransitionAt(1);
        }
    }
}