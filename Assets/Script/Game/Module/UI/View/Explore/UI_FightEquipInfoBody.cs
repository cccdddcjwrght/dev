/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Explore
{
    public partial class UI_FightEquipInfoBody : GLabel
    {
        public Controller m_type;
        public Controller m_quality;
        public GList m_attrs;
        public GTextField m_qstr;
        public const string URL = "ui://ow12is1hpm5b1e";

        public static UI_FightEquipInfoBody CreateInstance()
        {
            return (UI_FightEquipInfoBody)UIPackage.CreateObject("Explore", "FightEquipInfoBody");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_type = GetControllerAt(0);
            m_quality = GetControllerAt(1);
            m_attrs = (GList)GetChildAt(2);
            m_qstr = (GTextField)GetChildAt(5);
        }
    }
}