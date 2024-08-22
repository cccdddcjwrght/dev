/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Explore
{
    public partial class UI_FightReward : GLabel
    {
        public Controller m_isTip;
        public GLoader m_tip;
        public const string URL = "ui://ow12is1hkp2b1y";

        public static UI_FightReward CreateInstance()
        {
            return (UI_FightReward)UIPackage.CreateObject("Explore", "FightReward");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_isTip = GetControllerAt(0);
            m_tip = (GLoader)GetChildAt(4);
        }
    }
}