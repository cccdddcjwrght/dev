/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.GrowGift
{
    public partial class UI_Item : GComponent
    {
        public Controller m_sizesetting;
        public Controller m___disable;
        public Controller m_hideTitle;
        public GLoader m_icon;
        public GTextField m_title;
        public const string URL = "ui://862omg9yjfxha";

        public static UI_Item CreateInstance()
        {
            return (UI_Item)UIPackage.CreateObject("GrowGift", "Item");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_sizesetting = GetControllerAt(0);
            m___disable = GetControllerAt(1);
            m_hideTitle = GetControllerAt(2);
            m_icon = (GLoader)GetChildAt(0);
            m_title = (GTextField)GetChildAt(1);
        }
    }
}