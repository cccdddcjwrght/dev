/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Player
{
    public partial class UI_uplevelprogress : GProgressBar
    {
        public Controller m_state;
        public const string URL = "ui://cmw7t1elw46k1i";

        public static UI_uplevelprogress CreateInstance()
        {
            return (UI_uplevelprogress)UIPackage.CreateObject("Player", "uplevelprogress");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_state = GetControllerAt(0);
        }
    }
}