/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.BuffShop
{
    public partial class UI_BuffLotteryItem : GLabel
    {
        public Controller m_select;
        public GGraph m___effect;
        public const string URL = "ui://ay4imj6tq4m87";

        public static UI_BuffLotteryItem CreateInstance()
        {
            return (UI_BuffLotteryItem)UIPackage.CreateObject("BuffShop", "BuffLotteryItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_select = GetControllerAt(0);
            m___effect = (GGraph)GetChildAt(3);
        }
    }
}