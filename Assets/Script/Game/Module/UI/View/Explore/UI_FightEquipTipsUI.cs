/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Explore
{
    public partial class UI_FightEquipTipsUI : GLabel
    {
        public Controller m_type;
        public Controller m_size;
        public UI_FightEquipTipsBody m_old;
        public UI_FightEquipTipsBody m_info;
        public GButton m_drop;
        public GButton m_puton;
        public const string URL = "ui://ow12is1hpm5b1n";

        public static UI_FightEquipTipsUI CreateInstance()
        {
            return (UI_FightEquipTipsUI)UIPackage.CreateObject("Explore", "FightEquipTipsUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_type = GetControllerAt(0);
            m_size = GetControllerAt(1);
            m_old = (UI_FightEquipTipsBody)GetChildAt(2);
            m_info = (UI_FightEquipTipsBody)GetChildAt(3);
            m_drop = (GButton)GetChildAt(4);
            m_puton = (GButton)GetChildAt(5);
        }
    }
}