/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Player
{
    public partial class UI_EqUpHelpUI : GLabel
    {
        public GLabel m_body;
        public GList m_list;
        public const string URL = "ui://cmw7t1elkvvz6r";

        public static UI_EqUpHelpUI CreateInstance()
        {
            return (UI_EqUpHelpUI)UIPackage.CreateObject("Player", "EqUpHelpUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_body = (GLabel)GetChildAt(0);
            m_list = (GList)GetChildAt(1);
        }
    }
}