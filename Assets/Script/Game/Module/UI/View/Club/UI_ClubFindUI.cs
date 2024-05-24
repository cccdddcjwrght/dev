/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Club
{
    public partial class UI_ClubFindUI : GComponent
    {
        public Controller m_state;
        public GLabel m_body;
        public GTextInput m_input;
        public UI_IconBtn m_findBtn;
        public GList m_list;
        public UI_ClubBtn m_createBtn;
        public UI_ClubBtn m_joinBtn;
        public GGroup m_content;
        public const string URL = "ui://kgizakqqlu5m0";

        public static UI_ClubFindUI CreateInstance()
        {
            return (UI_ClubFindUI)UIPackage.CreateObject("Club", "ClubFindUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_state = GetControllerAt(0);
            m_body = (GLabel)GetChildAt(0);
            m_input = (GTextInput)GetChildAt(2);
            m_findBtn = (UI_IconBtn)GetChildAt(3);
            m_list = (GList)GetChildAt(7);
            m_createBtn = (UI_ClubBtn)GetChildAt(8);
            m_joinBtn = (UI_ClubBtn)GetChildAt(9);
            m_content = (GGroup)GetChildAt(10);
        }
    }
}