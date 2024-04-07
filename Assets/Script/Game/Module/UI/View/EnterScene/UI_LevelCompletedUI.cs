/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.EnterScene
{
    public partial class UI_LevelCompletedUI : GLabel
    {
        public GList m_list;
        public GButton m_click;
        public const string URL = "ui://cxpm3jfbudvnl";

        public static UI_LevelCompletedUI CreateInstance()
        {
            return (UI_LevelCompletedUI)UIPackage.CreateObject("EnterScene", "LevelCompletedUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_list = (GList)GetChildAt(5);
            m_click = (GButton)GetChildAt(6);
        }
    }
}