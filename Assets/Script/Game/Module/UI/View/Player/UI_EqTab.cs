/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Player
{
    public partial class UI_EqTab : GButton
    {
        public Controller m___redpoint;
        public const string URL = "ui://cmw7t1elk622z";

        public static UI_EqTab CreateInstance()
        {
            return (UI_EqTab)UIPackage.CreateObject("Player", "EqTab");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m___redpoint = GetControllerAt(1);
        }
    }
}