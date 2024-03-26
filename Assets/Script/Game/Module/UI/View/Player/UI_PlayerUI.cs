/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Player
{
    public partial class UI_PlayerUI : GLabel
    {
        public Controller m_eqTab;
        public GLabel m_body;
        public UI_EquipPage m_EquipPage;
        public UI_EquipUpQuality m_EquipQuality;
        public GList m_tabs;
        public GList m_list;
        public const string URL = "ui://cmw7t1elk6220";

        public static UI_PlayerUI CreateInstance()
        {
            return (UI_PlayerUI)UIPackage.CreateObject("Player", "PlayerUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_eqTab = GetControllerAt(0);
            m_body = (GLabel)GetChildAt(0);
            m_EquipPage = (UI_EquipPage)GetChildAt(1);
            m_EquipQuality = (UI_EquipUpQuality)GetChildAt(2);
            m_tabs = (GList)GetChildAt(4);
            m_list = (GList)GetChildAt(5);
        }
    }
}