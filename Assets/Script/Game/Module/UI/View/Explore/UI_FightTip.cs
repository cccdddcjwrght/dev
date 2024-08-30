/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Explore
{
    public partial class UI_FightTip : GLabel
    {
        public Controller m_state;
        public const string URL = "ui://ow12is1hdiea2o";

        public static UI_FightTip CreateInstance()
        {
            return (UI_FightTip)UIPackage.CreateObject("Explore", "FightTip");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_state = GetControllerAt(0);
        }
    }
}