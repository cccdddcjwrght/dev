/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Hud
{
    public partial class UI_FriendTipUI : GComponent
    {
        public Controller m_style;
        public GLoader m_icon;
        public const string URL = "ui://clbwsjawtwmrd";

        public static UI_FriendTipUI CreateInstance()
        {
            return (UI_FriendTipUI)UIPackage.CreateObject("Hud", "FriendTipUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_style = GetControllerAt(0);
            m_icon = (GLoader)GetChildAt(1);
        }
    }
}