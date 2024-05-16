/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.MonsterHunter
{
    public partial class UI_HunterBtn : GButton
    {
        public Controller m_auto;
        public const string URL = "ui://my7wqjw6twfoy";

        public static UI_HunterBtn CreateInstance()
        {
            return (UI_HunterBtn)UIPackage.CreateObject("MonsterHunter", "HunterBtn");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_auto = GetControllerAt(0);
        }
    }
}