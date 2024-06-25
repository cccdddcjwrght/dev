/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Common
{
    public partial class UI_BgIcon : GLabel
    {
        public Controller m_hidebg;
        public Controller m___redpoint;
        public Controller m_bgtype;
        public Controller m_lock;
        public GLoader m_bg;
        public GLoader m_red;
        public const string URL = "ui://2w8thcm7r4i17";

        public static UI_BgIcon CreateInstance()
        {
            return (UI_BgIcon)UIPackage.CreateObject("Common", "BgIcon");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_hidebg = GetControllerAt(0);
            m___redpoint = GetControllerAt(1);
            m_bgtype = GetControllerAt(2);
            m_lock = GetControllerAt(3);
            m_bg = (GLoader)GetChildAt(0);
            m_red = (GLoader)GetChildAt(2);
        }
    }
}