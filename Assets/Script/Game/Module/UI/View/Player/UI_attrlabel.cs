/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Player
{
    public partial class UI_attrlabel : GLabel
    {
        public Controller m_quality;
        public Controller m_lock;
        public const string URL = "ui://cmw7t1elw46k1j";

        public static UI_attrlabel CreateInstance()
        {
            return (UI_attrlabel)UIPackage.CreateObject("Player", "attrlabel");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_quality = GetControllerAt(0);
            m_lock = GetControllerAt(1);
        }
    }
}