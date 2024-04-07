/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.GameFriend
{
    public partial class UI_FriendUI : GComponent
    {
        public GLabel m_body;
        public GList m_listRecomment;
        public GList m_listFirends;
        public GTextField m_title1;
        public GTextField m_title2;
        public GTextField m_titleCount;
        public GTextField m_titleTime;
        public const string URL = "ui://nsy0h7irome20";

        public static UI_FriendUI CreateInstance()
        {
            return (UI_FriendUI)UIPackage.CreateObject("GameFriend", "FriendUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_body = (GLabel)GetChildAt(0);
            m_listRecomment = (GList)GetChildAt(2);
            m_listFirends = (GList)GetChildAt(3);
            m_title1 = (GTextField)GetChildAt(5);
            m_title2 = (GTextField)GetChildAt(6);
            m_titleCount = (GTextField)GetChildAt(7);
            m_titleTime = (GTextField)GetChildAt(8);
        }
    }
}