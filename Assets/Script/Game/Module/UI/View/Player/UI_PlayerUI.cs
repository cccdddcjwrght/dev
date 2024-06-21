/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Player
{
    public partial class UI_PlayerUI : GLabel
    {
        public Controller m_eqTab;
        public Controller m_c1;
        public Controller m_canmerge;
        public GLabel m_body;
        public GComponent m_GuideEmpty;
        public UI_EquipPage m_EquipPage;
        public UI_EquipUpQuality m_EquipQuality;
        public UI_EqTab m_info;
        public UI_EqTab m_equipup;
        public GList m_list;
        public GLoader m_clickBtn;
        public const string URL = "ui://cmw7t1elk6220";

        public static UI_PlayerUI CreateInstance()
        {
            return (UI_PlayerUI)UIPackage.CreateObject("Player", "PlayerUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_eqTab = GetControllerAt(0);
            m_c1 = GetControllerAt(1);
            m_canmerge = GetControllerAt(2);
            m_body = (GLabel)GetChildAt(0);
            m_GuideEmpty = (GComponent)GetChildAt(1);
            m_EquipPage = (UI_EquipPage)GetChildAt(2);
            m_EquipQuality = (UI_EquipUpQuality)GetChildAt(3);
            m_info = (UI_EqTab)GetChildAt(5);
            m_equipup = (UI_EqTab)GetChildAt(6);
            m_list = (GList)GetChildAt(7);
            m_clickBtn = (GLoader)GetChildAt(8);
        }
    }
}