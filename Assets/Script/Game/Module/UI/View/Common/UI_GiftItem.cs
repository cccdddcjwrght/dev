/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Common
{
    public partial class UI_GiftItem : GButton
    {
        public Controller m_IconSize;
        public Controller m_txtpos;
        public Controller m_color;
        public Controller m_txtSize;
        public UI_ColorText m_title_2;
        public UI_ColorText m_title_1;
        public const string URL = "ui://2w8thcm7hwwav";

        public static UI_GiftItem CreateInstance()
        {
            return (UI_GiftItem)UIPackage.CreateObject("Common", "GiftItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_IconSize = GetControllerAt(0);
            m_txtpos = GetControllerAt(1);
            m_color = GetControllerAt(2);
            m_txtSize = GetControllerAt(3);
            m_title_2 = (UI_ColorText)GetChildAt(1);
            m_title_1 = (UI_ColorText)GetChildAt(2);
        }
    }
}