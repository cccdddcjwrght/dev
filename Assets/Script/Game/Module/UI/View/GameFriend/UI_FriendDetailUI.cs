/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.GameFriend
{
    public partial class UI_FriendDetailUI : GComponent
    {
        public Controller m_recomment;
        public Controller m_comfirm;
        public UI_Comfirm m_comfirmDialog;
        public GButton m_btnClose;
        public GTextField m_title;
        public GButton m_btnDelete;
        public GButton m_btnOK;
        public GComponent m_Equip;
        public const string URL = "ui://nsy0h7irosc4s";

        public static UI_FriendDetailUI CreateInstance()
        {
            return (UI_FriendDetailUI)UIPackage.CreateObject("GameFriend", "FriendDetailUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_recomment = GetControllerAt(0);
            m_comfirm = GetControllerAt(1);
            m_comfirmDialog = (UI_Comfirm)GetChildAt(0);
            m_btnClose = (GButton)GetChildAt(2);
            m_title = (GTextField)GetChildAt(3);
            m_btnDelete = (GButton)GetChildAt(4);
            m_btnOK = (GButton)GetChildAt(5);
            m_Equip = (GComponent)GetChildAt(6);
        }
    }
}