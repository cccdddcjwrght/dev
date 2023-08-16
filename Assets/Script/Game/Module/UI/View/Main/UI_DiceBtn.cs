/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Main
{
    public partial class UI_DiceBtn : GButton
    {
        public Controller m_auto_dice;
        public GImage m_hold1;
        public GImage m_hold2;
        public const string URL = "ui://ktixaqljjx7gla5";

        public static UI_DiceBtn CreateInstance()
        {
            return (UI_DiceBtn)UIPackage.CreateObject("Main", "DiceBtn");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_auto_dice = GetControllerAt(1);
            m_hold1 = (GImage)GetChildAt(2);
            m_hold2 = (GImage)GetChildAt(3);
        }
    }
}