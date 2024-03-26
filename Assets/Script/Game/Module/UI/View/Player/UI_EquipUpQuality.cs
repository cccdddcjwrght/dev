/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Player
{
    public partial class UI_EquipUpQuality : GComponent
    {
        public Controller m_state;
        public GProgressBar m_progress;
        public UI_Equip m_selecteq;
        public UI_Equip m_nexteq;
        public GButton m_cleareq;
        public GButton m_click;
        public GTextField m_nextattr;
        public const string URL = "ui://cmw7t1elmk8f1m";

        public static UI_EquipUpQuality CreateInstance()
        {
            return (UI_EquipUpQuality)UIPackage.CreateObject("Player", "EquipUpQuality");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_state = GetControllerAt(0);
            m_progress = (GProgressBar)GetChildAt(1);
            m_selecteq = (UI_Equip)GetChildAt(2);
            m_nexteq = (UI_Equip)GetChildAt(3);
            m_cleareq = (GButton)GetChildAt(4);
            m_click = (GButton)GetChildAt(5);
            m_nextattr = (GTextField)GetChildAt(6);
        }
    }
}