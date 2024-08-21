/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Explore
{
    public partial class UI_FightEquipTipsBody : GLabel
    {
        public Controller m_new;
        public Controller m_upstate;
        public Controller m_bgtype;
        public GTextField m_name;
        public GList m_attrs;
        public GTextField m_power;
        public const string URL = "ui://ow12is1hpm5b1o";

        public static UI_FightEquipTipsBody CreateInstance()
        {
            return (UI_FightEquipTipsBody)UIPackage.CreateObject("Explore", "FightEquipTipsBody");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_new = GetControllerAt(0);
            m_upstate = GetControllerAt(1);
            m_bgtype = GetControllerAt(2);
            m_name = (GTextField)GetChildAt(5);
            m_attrs = (GList)GetChildAt(6);
            m_power = (GTextField)GetChildAt(7);
        }
    }
}