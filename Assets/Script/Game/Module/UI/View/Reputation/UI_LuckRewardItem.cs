/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Reputation
{
    public partial class UI_LuckRewardItem : GLabel
    {
        public Controller m_bg;
        public Controller m_color;
        public GGraph m___effect;
        public const string URL = "ui://vnok3a30khnp1d";

        public static UI_LuckRewardItem CreateInstance()
        {
            return (UI_LuckRewardItem)UIPackage.CreateObject("Reputation", "LuckRewardItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_bg = GetControllerAt(0);
            m_color = GetControllerAt(1);
            m___effect = (GGraph)GetChildAt(0);
        }
    }
}