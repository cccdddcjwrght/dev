/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.EnterScene
{
    public partial class UI_WelcomeNewLevelUI : GLabel
    {
        public GButton m_click;
        public const string URL = "ui://cxpm3jfbudvnt";

        public static UI_WelcomeNewLevelUI CreateInstance()
        {
            return (UI_WelcomeNewLevelUI)UIPackage.CreateObject("EnterScene", "WelcomeNewLevelUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_click = (GButton)GetChildAt(1);
        }
    }
}