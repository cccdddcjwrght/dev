/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace GameFriend
{
    public partial class UI_FriendItem : GComponent
    {
        public Controller m_state;
        public GImage m_bg;
        public GImage m_bgselected;
        public GButton m_head;
        public GTextField m_name;
        public GList m_equips;
        public GButton m_btnNO;
        public GButton m_btnYES;
        public GTextField m_titleHiretoday;
        public GButton m_btnHire;
        public GButton m_btnHiring;
        public GImage m_iconHiring;
        public GTextField m_titleHiring;
        public const string URL = "ui://nsy0h7irome22";

        public static UI_FriendItem CreateInstance()
        {
            return (UI_FriendItem)UIPackage.CreateObject("GameFriend", "FriendItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_state = GetControllerAt(0);
            m_bg = (GImage)GetChildAt(0);
            m_bgselected = (GImage)GetChildAt(1);
            m_head = (GButton)GetChildAt(2);
            m_name = (GTextField)GetChildAt(3);
            m_equips = (GList)GetChildAt(4);
            m_btnNO = (GButton)GetChildAt(5);
            m_btnYES = (GButton)GetChildAt(6);
            m_titleHiretoday = (GTextField)GetChildAt(7);
            m_btnHire = (GButton)GetChildAt(8);
            m_btnHiring = (GButton)GetChildAt(9);
            m_iconHiring = (GImage)GetChildAt(10);
            m_titleHiring = (GTextField)GetChildAt(11);
        }
    }
}