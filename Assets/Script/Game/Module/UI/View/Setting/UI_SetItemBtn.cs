/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Setting
{
    public partial class UI_SetItemBtn : GButton
    {
        public Controller m_btn;
        public GLoader m_del;
        public GLoader m_icon;
        public GRichTextField m_title;
        public UI_SwitchBtn m_toggle;
        public UI_NormalBtn m_nomal;
        public const string URL = "ui://dpgt0e2zn1bmt";

        public static UI_SetItemBtn CreateInstance()
        {
            return (UI_SetItemBtn)UIPackage.CreateObject("Setting", "SetItemBtn");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_btn = GetControllerAt(0);
            m_del = (GLoader)GetChildAt(0);
            m_icon = (GLoader)GetChildAt(1);
            m_title = (GRichTextField)GetChildAt(2);
            m_toggle = (UI_SwitchBtn)GetChildAt(3);
            m_nomal = (UI_NormalBtn)GetChildAt(4);
        }
    }
}