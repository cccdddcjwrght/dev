/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Reputation
{
    public partial class UI_LuckShow : GLabel
    {
        public GGraph m_mask;
        public UI_LuckRewardItem m_reward;
        public const string URL = "ui://vnok3a308z8h1j";

        public static UI_LuckShow CreateInstance()
        {
            return (UI_LuckShow)UIPackage.CreateObject("Reputation", "LuckShow");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_mask = (GGraph)GetChildAt(0);
            m_reward = (UI_LuckRewardItem)GetChildAt(1);
        }
    }
}