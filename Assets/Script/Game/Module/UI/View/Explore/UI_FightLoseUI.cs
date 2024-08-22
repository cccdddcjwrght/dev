/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Explore
{
    public partial class UI_FightLoseUI : GLabel
    {
        public GLabel m_body;
        public GButton m_confirmBtn;
        public const string URL = "ui://ow12is1hkp2b1t";

        public static UI_FightLoseUI CreateInstance()
        {
            return (UI_FightLoseUI)UIPackage.CreateObject("Explore", "FightLoseUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_body = (GLabel)GetChildAt(0);
            m_confirmBtn = (GButton)GetChildAt(4);
        }
    }
}