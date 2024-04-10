/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Player
{
    public partial class UI_MatDiv : GComponent
    {
        public Controller m_type;
        public const string URL = "ui://cmw7t1elwaj63m";

        public static UI_MatDiv CreateInstance()
        {
            return (UI_MatDiv)UIPackage.CreateObject("Player", "MatDiv");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_type = GetControllerAt(0);
        }
    }
}