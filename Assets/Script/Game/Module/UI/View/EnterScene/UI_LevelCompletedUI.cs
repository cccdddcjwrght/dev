/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.EnterScene
{
    public partial class UI_LevelCompletedUI : GLabel
    {
        public GLoader m_build;
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

            m_build = (GLoader)GetChildAt(0);
            m_body = (UI_LevelCompletedBody)GetChildAt(2);
            m_doshow = GetTransitionAt(0);
            m_dohide = GetTransitionAt(1);
        }
    }
}