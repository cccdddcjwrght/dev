/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Player
{
    public partial class UI_EquipTipsUI : GLabel
    {
        public Controller m_quality;
        public Controller m_lvmax;
        public Controller m_hide;
        public GGraph m_maskbg;
        public GTextField m_level;
        public GTextField m_attr;
        public GTextField m_qualitytips;
        public GTextField m_nextlvattr;
        public GTextField m_cost;
        public GLoader m_item;
        public UI_uplevelprogress m_progress;
        public GList m_list;
        public GButton m_click;
        public GButton m_up;
        public GButton m_close;
        public GButton m_click2;
        public const string URL = "ui://cmw7t1elw46k1h";

        public static UI_EquipTipsUI CreateInstance()
        {
            return (UI_EquipTipsUI)UIPackage.CreateObject("Player", "EquipTipsUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_quality = GetControllerAt(0);
            m_lvmax = GetControllerAt(1);
            m_hide = GetControllerAt(2);
            m_maskbg = (GGraph)GetChildAt(0);
            m_level = (GTextField)GetChildAt(4);
            m_attr = (GTextField)GetChildAt(6);
            m_qualitytips = (GTextField)GetChildAt(7);
            m_nextlvattr = (GTextField)GetChildAt(8);
            m_cost = (GTextField)GetChildAt(9);
            m_item = (GLoader)GetChildAt(11);
            m_progress = (UI_uplevelprogress)GetChildAt(12);
            m_list = (GList)GetChildAt(16);
            m_click = (GButton)GetChildAt(17);
            m_up = (GButton)GetChildAt(18);
            m_close = (GButton)GetChildAt(19);
            m_click2 = (GButton)GetChildAt(20);
        }
    }
}