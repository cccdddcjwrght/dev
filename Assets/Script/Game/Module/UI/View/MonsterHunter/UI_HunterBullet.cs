/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.MonsterHunter
{
    public partial class UI_HunterBullet : GLabel
    {
        public Controller m_type;
        public const string URL = "ui://my7wqjw6twfo19";

        public static UI_HunterBullet CreateInstance()
        {
            return (UI_HunterBullet)UIPackage.CreateObject("MonsterHunter", "HunterBullet");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_type = GetControllerAt(0);
        }
    }
}