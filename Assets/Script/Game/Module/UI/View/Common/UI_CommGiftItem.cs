/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Common
{
    public partial class UI_CommGiftItem : GComponent
    {
        public Controller m_sizesetting;
        public Controller m___disable;
        public Controller m_tipicon;
        public GLoader m_icon;
        public GTextField m_title;
        public GLoader m_btnInfo;
        public const string URL = "ui://2w8thcm7s86pi";

        public static UI_CommGiftItem CreateInstance()
        {
            return (UI_CommGiftItem)UIPackage.CreateObject("Common", "CommGiftItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_sizesetting = GetControllerAt(0);
            m___disable = GetControllerAt(1);
            m_tipicon = GetControllerAt(2);
            m_icon = (GLoader)GetChildAt(0);
            m_title = (GTextField)GetChildAt(2);
            m_btnInfo = (GLoader)GetChildAt(3);
        }
    }
}