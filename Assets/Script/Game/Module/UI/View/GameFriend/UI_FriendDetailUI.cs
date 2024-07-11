/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.GameFriend
{
    public partial class UI_FriendDetailUI : GComponent
    {
        public Controller m_recomment;
        public Controller m_comfirm;
        public GTextField m_title;
        public GComponent m_Equip;
        public GButton m_close;
        public GButton m_btnOK;
        public GButton m_btnDelete;
        public UI_Comfirm m_comfirmDialog;
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
            m_title = (GTextField)GetChildAt(2);
            m_Equip = (GComponent)GetChildAt(3);
            m_close = (GButton)GetChildAt(4);
            m_btnOK = (GButton)GetChildAt(5);
            m_btnDelete = (GButton)GetChildAt(6);
            m_comfirmDialog = (UI_Comfirm)GetChildAt(8);
        }
    }
}