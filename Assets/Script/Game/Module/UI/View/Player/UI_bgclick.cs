/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Player
{
    public partial class UI_bgclick : GLabel
    {
        public Controller m_bg;
        public const string URL = "ui://cmw7t1elk62216";

        public static UI_bgclick CreateInstance()
        {
            return (UI_bgclick)UIPackage.CreateObject("Player", "bgclick");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_bg = GetControllerAt(0);
        }
    }
}