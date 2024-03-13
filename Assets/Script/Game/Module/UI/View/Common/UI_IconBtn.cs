/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Common
{
    public partial class UI_IconBtn : GButton
    {
        public Controller m_button;
        public GLoader m_icon;
        public const string URL = "ui://2w8thcm7n1bm3lbu";

        public static UI_IconBtn CreateInstance()
        {
            return (UI_IconBtn)UIPackage.CreateObject("Common", "IconBtn");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_button = GetControllerAt(0);
            m_icon = (GLoader)GetChildAt(0);
        }
    }
}