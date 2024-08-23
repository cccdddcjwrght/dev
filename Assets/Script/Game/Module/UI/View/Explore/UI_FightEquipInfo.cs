/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Explore
{
    public partial class UI_FightEquipInfo : GLabel
    {
        public Controller m_type;
        public UI_FightEquipInfoBody m_body;
        public Transition m_show;
        public const string URL = "ui://ow12is1hpm5b1f";

        public static UI_FightEquipInfo CreateInstance()
        {
            return (UI_FightEquipInfo)UIPackage.CreateObject("Explore", "FightEquipInfo");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_type = GetControllerAt(0);
            m_body = (UI_FightEquipInfoBody)GetChildAt(0);
            m_show = GetTransitionAt(0);
        }
    }
}