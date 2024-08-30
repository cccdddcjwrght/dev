/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Explore
{
    public partial class UI_FightGiftUI : GLabel
    {
        public UI_GiftBody m_body;
        public Transition m_doshow;
        public Transition m_dohide;
        public const string URL = "ui://ow12is1hdiea2q";

        public static UI_FightGiftUI CreateInstance()
        {
            return (UI_FightGiftUI)UIPackage.CreateObject("Explore", "FightGiftUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_body = (UI_GiftBody)GetChildAt(1);
            m_doshow = GetTransitionAt(0);
            m_dohide = GetTransitionAt(1);
        }
    }
}