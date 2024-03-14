/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Player
{
    public partial class UI_EquipGiftUI : GLabel
    {
        public Controller m_type;
        public GGraph m_mask;
        public UI_GiftBody m_body;
        public Transition m_open;
        public Transition m_t1;
        public const string URL = "ui://cmw7t1elw46k17";

        public static UI_EquipGiftUI CreateInstance()
        {
            return (UI_EquipGiftUI)UIPackage.CreateObject("Player", "EquipGiftUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_type = GetControllerAt(0);
            m_mask = (GGraph)GetChildAt(0);
            m_body = (UI_GiftBody)GetChildAt(1);
            m_open = GetTransitionAt(0);
            m_t1 = GetTransitionAt(1);
        }
    }
}