/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.EnterScene
{
    public partial class UI_LevelCompletedBody : GLabel
    {
        public GGraph m___effect;
        public GGraph m___effect1;
        public GList m_list;
        public GButton m_click;
        public Transition m_doshow;
        public Transition m_dohide;
        public const string URL = "ui://cxpm3jfbss9b12";

        public static UI_LevelCompletedBody CreateInstance()
        {
            return (UI_LevelCompletedBody)UIPackage.CreateObject("EnterScene", "LevelCompletedBody");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m___effect = (GGraph)GetChildAt(0);
            m___effect1 = (GGraph)GetChildAt(1);
            m_list = (GList)GetChildAt(8);
            m_click = (GButton)GetChildAt(9);
            m_doshow = GetTransitionAt(0);
            m_dohide = GetTransitionAt(1);
        }
    }
}