/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Hud
{
    public partial class UI_OrderTip : GComponent
    {
        public GImage m_n0;
        public GLoader m_n2;
        public const string URL = "ui://clbwsjawlrli2";

        public static UI_OrderTip CreateInstance()
        {
            return (UI_OrderTip)UIPackage.CreateObject("Hud", "OrderTip");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_n0 = (GImage)GetChildAt(0);
            m_n2 = (GLoader)GetChildAt(1);
        }
    }
}