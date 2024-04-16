/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.GrowGift
{
    public partial class UI_GiftItem : GButton
    {
        public Controller m_state;
        public GProgressBar m_progress;
        public UI_Item m_gift_icon;
        public const string URL = "ui://862omg9yjfxh6";

        public static UI_GiftItem CreateInstance()
        {
            return (UI_GiftItem)UIPackage.CreateObject("GrowGift", "GiftItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_state = GetControllerAt(0);
            m_progress = (GProgressBar)GetChildAt(4);
            m_gift_icon = (UI_Item)GetChildAt(6);
        }
    }
}