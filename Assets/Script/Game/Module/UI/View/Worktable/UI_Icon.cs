/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Worktable
{
    public partial class UI_Icon : GLabel
    {
        public Controller m_type;
        public GLoader m_icon;
        public const string URL = "ui://m8rpv7f9b32e5";

        public static UI_Icon CreateInstance()
        {
            return (UI_Icon)UIPackage.CreateObject("Worktable", "Icon");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_type = GetControllerAt(0);
            m_icon = (GLoader)GetChildAt(0);
        }
    }
}