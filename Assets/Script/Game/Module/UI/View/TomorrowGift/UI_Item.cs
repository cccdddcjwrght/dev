/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.TomorrowGift
{
    public partial class UI_Item : GComponent
    {
        public Controller m_sizesetting;
        public Controller m___disable;
        public Controller m_tipicon;
        public GTextField m_title;
        public GLoader m_icon;
        public GLoader m_btnInfo;
        public const string URL = "ui://7crbg35hosc45";

        public static UI_Item CreateInstance()
        {
            return (UI_Item)UIPackage.CreateObject("TomorrowGift", "Item");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_sizesetting = GetControllerAt(0);
            m___disable = GetControllerAt(1);
            m_tipicon = GetControllerAt(2);
            m_title = (GTextField)GetChildAt(1);
            m_icon = (GLoader)GetChildAt(2);
            m_btnInfo = (GLoader)GetChildAt(3);
        }
    }
}