/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Player
{
    public partial class UI_EquipUpLabel : GLabel
    {
        public GTextField m_val;
        public GTextField m_next;
        public const string URL = "ui://cmw7t1elv1il78";

        public static UI_EquipUpLabel CreateInstance()
        {
            return (UI_EquipUpLabel)UIPackage.CreateObject("Player", "EquipUpLabel");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_val = (GTextField)GetChildAt(3);
            m_next = (GTextField)GetChildAt(4);
        }
    }
}