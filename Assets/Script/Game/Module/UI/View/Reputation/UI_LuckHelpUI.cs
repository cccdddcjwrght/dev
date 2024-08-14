/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Reputation
{
    public partial class UI_LuckHelpUI : GLabel
    {
        public GGraph m_mask;
        public GGroup m_content;
        public const string URL = "ui://vnok3a30vlyv1m";

        public static UI_LuckHelpUI CreateInstance()
        {
            return (UI_LuckHelpUI)UIPackage.CreateObject("Reputation", "LuckHelpUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_mask = (GGraph)GetChildAt(0);
            m_content = (GGroup)GetChildAt(10);
        }
    }
}