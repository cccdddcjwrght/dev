/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Setting
{
    public partial class UI_SetItem : GButton
    {
        public Controller m_btn;
        public UI_SetItemBtn m_body;
        public const string URL = "ui://dpgt0e2zn1bms";

        public static UI_SetItem CreateInstance()
        {
            return (UI_SetItem)UIPackage.CreateObject("Setting", "SetItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_btn = GetControllerAt(0);
            m_body = (UI_SetItemBtn)GetChildAt(0);
        }
    }
}