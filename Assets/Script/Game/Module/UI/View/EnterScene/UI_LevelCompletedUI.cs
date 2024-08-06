/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.EnterScene
{
    public partial class UI_LevelCompletedUI : GLabel
    {
        public GLoader m_buildold;
        public GLoader m_build;
        public GGraph m___effect;
        public UI_LevelCompletedBody m_body;
        public Transition m_doshow;
        public Transition m_dohide;
        public const string URL = "ui://cxpm3jfbudvnl";

        public static UI_LevelCompletedUI CreateInstance()
        {
            return (UI_LevelCompletedUI)UIPackage.CreateObject("EnterScene", "LevelCompletedUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_buildold = (GLoader)GetChildAt(0);
            m_build = (GLoader)GetChildAt(1);
            m___effect = (GGraph)GetChildAt(2);
            m_body = (UI_LevelCompletedBody)GetChildAt(4);
            m_doshow = GetTransitionAt(0);
            m_dohide = GetTransitionAt(1);
        }
    }
}