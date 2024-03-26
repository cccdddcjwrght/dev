/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.GameFriend
{
    public partial class UI_FriendUI : GComponent
    {
        public GLabel m_bg;
        public GList m_listRecomment;
        public GList m_listFirends;
        public const string URL = "ui://nsy0h7irome20";

        public static UI_FriendUI CreateInstance()
        {
            return (UI_FriendUI)UIPackage.CreateObject("GameFriend", "FriendUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_bg = (GLabel)GetChildAt(0);
            m_listRecomment = (GList)GetChildAt(2);
            m_listFirends = (GList)GetChildAt(3);
        }
    }
}