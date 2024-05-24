/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Club
{
    public partial class UI_ClubSelectUI : GComponent
    {
        public Controller m_tab;
        public GLabel m_body;
        public UI_ClubIcon m_clubIcon;
        public GButton m_iconTab;
        public GButton m_frameTab;
        public GList m_headList;
        public GList m_frameList;
        public GGroup m_content;
        public const string URL = "ui://kgizakqqlu5m1k";

        public static UI_ClubSelectUI CreateInstance()
        {
            return (UI_ClubSelectUI)UIPackage.CreateObject("Club", "ClubSelectUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_tab = GetControllerAt(0);
            m_body = (GLabel)GetChildAt(0);
            m_clubIcon = (UI_ClubIcon)GetChildAt(2);
            m_iconTab = (GButton)GetChildAt(3);
            m_frameTab = (GButton)GetChildAt(4);
            m_headList = (GList)GetChildAt(6);
            m_frameList = (GList)GetChildAt(7);
            m_content = (GGroup)GetChildAt(8);
        }
    }
}