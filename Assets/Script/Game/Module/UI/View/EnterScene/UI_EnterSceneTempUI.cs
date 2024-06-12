/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.EnterScene
{
    public partial class UI_EnterSceneTempUI : GComponent
    {
        public GList m_list;
        public GTextField m_title;
        public GButton m_close;
        public GGroup m_titleGroup;
        public const string URL = "ui://cxpm3jfbq90g2z";

        public static UI_EnterSceneTempUI CreateInstance()
        {
            return (UI_EnterSceneTempUI)UIPackage.CreateObject("EnterScene", "EnterSceneTempUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_list = (GList)GetChildAt(3);
            m_title = (GTextField)GetChildAt(5);
            m_close = (GButton)GetChildAt(6);
            m_titleGroup = (GGroup)GetChildAt(7);
        }
    }
}