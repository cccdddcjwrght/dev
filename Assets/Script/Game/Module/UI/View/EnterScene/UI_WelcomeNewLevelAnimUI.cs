/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.EnterScene
{
    public partial class UI_WelcomeNewLevelAnimUI : GLabel
    {
        public GLoader m_anim;
        public GImage m_next;
        public Transition m_dohide;
        public const string URL = "ui://cxpm3jfb8pk228";

        public static UI_WelcomeNewLevelAnimUI CreateInstance()
        {
            return (UI_WelcomeNewLevelAnimUI)UIPackage.CreateObject("EnterScene", "WelcomeNewLevelAnimUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_anim = (GLoader)GetChildAt(1);
            m_next = (GImage)GetChildAt(2);
            m_dohide = GetTransitionAt(0);
        }
    }
}