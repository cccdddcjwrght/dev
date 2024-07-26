/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.HotFood
{
    public partial class UI_HotFoodUI : GComponent
    {
        public Controller m_hoting;
        public GLabel m_body;
        public GTextField m_cdtime;
        public GTextField m_duration;
        public GTextField m_foodTip;
        public GTextField m_des;
        public GButton m_startBtn;
        public GButton m_stopBtn;
        public GList m_list;
        public GLoader m_icon;
        public GImage m_progress;
        public GGroup m_hot_food;
        public GGroup m_content;
        public const string URL = "ui://zo86yb1ivlyvb";

        public static UI_HotFoodUI CreateInstance()
        {
            return (UI_HotFoodUI)UIPackage.CreateObject("HotFood", "HotFoodUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_hoting = GetControllerAt(0);
            m_body = (GLabel)GetChildAt(0);
            m_cdtime = (GTextField)GetChildAt(7);
            m_duration = (GTextField)GetChildAt(9);
            m_foodTip = (GTextField)GetChildAt(10);
            m_des = (GTextField)GetChildAt(11);
            m_startBtn = (GButton)GetChildAt(12);
            m_stopBtn = (GButton)GetChildAt(13);
            m_list = (GList)GetChildAt(14);
            m_icon = (GLoader)GetChildAt(16);
            m_progress = (GImage)GetChildAt(17);
            m_hot_food = (GGroup)GetChildAt(18);
            m_content = (GGroup)GetChildAt(19);
        }
    }
}