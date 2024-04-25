/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Player
{
    public partial class UI_EquipBox : GButton
    {
        public Controller m_quality;
        public Controller m___redpoint;
        public GGraph m_size;
        public UI_Equip m_body;
        public Transition m_show;
        public const string URL = "ui://cmw7t1elw46k1e";

        public static UI_EquipBox CreateInstance()
        {
            return (UI_EquipBox)UIPackage.CreateObject("Player", "EquipBox");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_quality = GetControllerAt(0);
            m___redpoint = GetControllerAt(1);
            m_size = (GGraph)GetChildAt(0);
            m_body = (UI_Equip)GetChildAt(1);
            m_show = GetTransitionAt(0);
        }
    }
}