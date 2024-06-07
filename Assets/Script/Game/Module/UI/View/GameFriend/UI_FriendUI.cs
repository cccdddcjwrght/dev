/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.GameFriend
{
    public partial class UI_FriendUI : GComponent
    {
        public Controller m_empty;
        public Controller m_emptyFriend;
        public Controller m_allEmpty;
        public GLabel m_body;
        public GImage m_topBar;
        public GTextField m_title1;
        public GList m_listRecomment;
        public GList m_listFirends;
        public GTextField m_title2;
        public GTextField m_titleTime;
        public GTextField m_titleCount;
        public GGroup m_friendGroup;
        public const string URL = "ui://nsy0h7irome20";

        public static UI_FriendUI CreateInstance()
        {
            return (UI_FriendUI)UIPackage.CreateObject("GameFriend", "FriendUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_empty = GetControllerAt(0);
            m_emptyFriend = GetControllerAt(1);
            m_allEmpty = GetControllerAt(2);
            m_body = (GLabel)GetChildAt(0);
            m_topBar = (GImage)GetChildAt(1);
            m_title1 = (GTextField)GetChildAt(2);
            m_listRecomment = (GList)GetChildAt(3);
            m_listFirends = (GList)GetChildAt(4);
            m_title2 = (GTextField)GetChildAt(6);
            m_titleTime = (GTextField)GetChildAt(7);
            m_titleCount = (GTextField)GetChildAt(8);
            m_friendGroup = (GGroup)GetChildAt(9);
        }
    }
}