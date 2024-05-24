/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Club
{
    public partial class UI_ClubMemberItem : GComponent
    {
        public Controller m_isMember;
        public Controller m_isSelf;
        public Controller m_isFriend;
        public GButton m_head;
        public GLoader m_currencyIcon;
        public GTextField m_name;
        public GTextField m_value;
        public const string URL = "ui://kgizakqqlu5m28";

        public static UI_ClubMemberItem CreateInstance()
        {
            return (UI_ClubMemberItem)UIPackage.CreateObject("Club", "ClubMemberItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_isMember = GetControllerAt(0);
            m_isSelf = GetControllerAt(1);
            m_isFriend = GetControllerAt(2);
            m_head = (GButton)GetChildAt(1);
            m_currencyIcon = (GLoader)GetChildAt(4);
            m_name = (GTextField)GetChildAt(5);
            m_value = (GTextField)GetChildAt(6);
        }
    }
}