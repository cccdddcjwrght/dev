/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Hud
{
    public partial class UI_OrderTip : GComponent
    {
        public Controller m_friend;
        public GLoader m_icon;
        public GTextField m_num;
        public const string URL = "ui://clbwsjawlrli2";

        public static UI_OrderTip CreateInstance()
        {
            return (UI_OrderTip)UIPackage.CreateObject("Hud", "OrderTip");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_friend = GetControllerAt(0);
            m_icon = (GLoader)GetChildAt(1);
            m_num = (GTextField)GetChildAt(3);
        }
    }
}