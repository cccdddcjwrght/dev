/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Club
{
    public partial class UI_ClubItem : GLabel
    {
        public Controller m_state;
        public UI_ClubIcon m_clubIcon;
        public GTextField m_name;
        public GTextField m_ID;
        public GTextField m_count;
        public UI_ClubBtn m_joinBtn;
        public UI_IconBtn m_leaveBtn;
        public const string URL = "ui://kgizakqqlu5m1f";

        public static UI_ClubItem CreateInstance()
        {
            return (UI_ClubItem)UIPackage.CreateObject("Club", "ClubItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_state = GetControllerAt(0);
            m_clubIcon = (UI_ClubIcon)GetChildAt(1);
            m_name = (GTextField)GetChildAt(2);
            m_ID = (GTextField)GetChildAt(3);
            m_count = (GTextField)GetChildAt(5);
            m_joinBtn = (UI_ClubBtn)GetChildAt(6);
            m_leaveBtn = (UI_IconBtn)GetChildAt(7);
        }
    }
}