/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Player
{
    public partial class UI_Equip : GButton
    {
        public Controller m_quality;
        public Controller m___redpoint;
        public Controller m_type;
        public Controller m_select;
        public GTextField m_qname;
        public GTextField m_level;
        public const string URL = "ui://cmw7t1elk62214";

        public static UI_Equip CreateInstance()
        {
            return (UI_Equip)UIPackage.CreateObject("Player", "Equip");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_quality = GetControllerAt(0);
            m___redpoint = GetControllerAt(1);
            m_type = GetControllerAt(2);
            m_select = GetControllerAt(3);
            m_qname = (GTextField)GetChildAt(5);
            m_level = (GTextField)GetChildAt(6);
        }
    }
}