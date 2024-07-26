/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Player
{
    public partial class UI_EquipPreviewUI : GLabel
    {
        public GLabel m_body;
        public UI_Equip m_equip;
        public UI_Equip m_nextequip;
        public UI_EquipUpLabel m_level;
        public UI_EquipUpLabel m_main;
        public UI_attrlabel m_attr;
        public const string URL = "ui://cmw7t1elv1il77";

        public static UI_EquipPreviewUI CreateInstance()
        {
            return (UI_EquipPreviewUI)UIPackage.CreateObject("Player", "EquipPreviewUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_body = (GLabel)GetChildAt(0);
            m_equip = (UI_Equip)GetChildAt(1);
            m_nextequip = (UI_Equip)GetChildAt(2);
            m_level = (UI_EquipUpLabel)GetChildAt(5);
            m_main = (UI_EquipUpLabel)GetChildAt(6);
            m_attr = (UI_attrlabel)GetChildAt(8);
        }
    }
}