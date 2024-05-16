/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.MonsterHunter
{
    public partial class UI_HpHud : GLabel
    {
        public Transition m_t0;
        public const string URL = "ui://my7wqjw6tdsblbg";

        public static UI_HpHud CreateInstance()
        {
            return (UI_HpHud)UIPackage.CreateObject("MonsterHunter", "HpHud");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_t0 = GetTransitionAt(0);
        }
    }
}