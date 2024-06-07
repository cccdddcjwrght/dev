/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.EnterScene
{
    public partial class UI_EnterSceneTempUI : GComponent
    {
        public GTextField m_t1;
        public GTextField m_t2;
        public GTextField m_t3;
        public GTextField m_title;
        public GGroup m_titleGroup;
        public GButton m_close;
        public const string URL = "ui://cxpm3jfbq90g2z";

        public static UI_EnterSceneTempUI CreateInstance()
        {
            return (UI_EnterSceneTempUI)UIPackage.CreateObject("EnterScene", "EnterSceneTempUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_t1 = (GTextField)GetChildAt(1);
            m_t2 = (GTextField)GetChildAt(2);
            m_t3 = (GTextField)GetChildAt(3);
            m_title = (GTextField)GetChildAt(6);
            m_titleGroup = (GGroup)GetChildAt(7);
            m_close = (GButton)GetChildAt(8);
        }
    }
}