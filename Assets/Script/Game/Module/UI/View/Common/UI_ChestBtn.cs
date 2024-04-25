/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Common
{
    public partial class UI_ChestBtn : GButton
    {
        public Controller m___redpoint;
        public UI_ChestReward m_body;
        public const string URL = "ui://2w8thcm7lhn63leb";

        public static UI_ChestBtn CreateInstance()
        {
            return (UI_ChestBtn)UIPackage.CreateObject("Common", "ChestBtn");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m___redpoint = GetControllerAt(0);
            m_body = (UI_ChestReward)GetChildAt(0);
        }
    }
}