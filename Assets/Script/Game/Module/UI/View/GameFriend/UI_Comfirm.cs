/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.GameFriend
{
    public partial class UI_Comfirm : GComponent
    {
        public GGraph m_body;
        public GTextField m_titleName;
        public GButton m_btnCancle;
        public GButton m_btnOK;
        public const string URL = "ui://nsy0h7irjr1qu";

        public static UI_Comfirm CreateInstance()
        {
            return (UI_Comfirm)UIPackage.CreateObject("GameFriend", "Comfirm");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_body = (GGraph)GetChildAt(0);
            m_titleName = (GTextField)GetChildAt(4);
            m_btnCancle = (GButton)GetChildAt(5);
            m_btnOK = (GButton)GetChildAt(6);
        }
    }
}