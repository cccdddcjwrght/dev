/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.MonsterHunter
{
    public partial class UI_HunterRewardItem : GLabel
    {
        public Controller m_type;
        public Transition m_show;
        public const string URL = "ui://my7wqjw6dfdxlbb";

        public static UI_HunterRewardItem CreateInstance()
        {
            return (UI_HunterRewardItem)UIPackage.CreateObject("MonsterHunter", "HunterRewardItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_type = GetControllerAt(0);
            m_show = GetTransitionAt(0);
        }
    }
}