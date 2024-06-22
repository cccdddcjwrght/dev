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
        public GGroup m_title_group;
        public GImage m_bar;
        public GTextField m_tip;
        public GGroup m_tip_group;
        public const string URL = "ui://cxpm3jfbq90g2z";

        public static UI_EnterSceneTempUI CreateInstance()
        {
            return (UI_EnterSceneTempUI)UIPackage.CreateObject("EnterScene", "EnterSceneTempUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_list = (GList)GetChildAt(1);
            m_title = (GTextField)GetChildAt(4);
            m_close = (GButton)GetChildAt(5);
            m_title_group = (GGroup)GetChildAt(6);
            m_bar = (GImage)GetChildAt(8);
            m_tip = (GTextField)GetChildAt(9);
            m_tip_group = (GGroup)GetChildAt(10);
        }
    }
}