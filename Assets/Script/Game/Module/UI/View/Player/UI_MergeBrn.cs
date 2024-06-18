/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Player
{
    public partial class UI_MergeBrn : GButton
    {
        public Controller m___redpoint;
        public const string URL = "ui://cmw7t1elfluu54";

        public static UI_MergeBrn CreateInstance()
        {
            return (UI_MergeBrn)UIPackage.CreateObject("Player", "MergeBrn");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m___redpoint = GetControllerAt(0);
        }
    }
}