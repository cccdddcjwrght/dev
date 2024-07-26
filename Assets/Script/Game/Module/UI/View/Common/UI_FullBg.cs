/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Common
{
    public partial class UI_FullBg : GLabel
    {
        public UI_Currency m_currency;
        public UI_CloseBtn m_close;
        public const string URL = "ui://2w8thcm7adfx3lko";

        public static UI_FullBg CreateInstance()
        {
            return (UI_FullBg)UIPackage.CreateObject("Common", "FullBg");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_currency = (UI_Currency)GetChildAt(1);
            m_close = (UI_CloseBtn)GetChildAt(2);
        }
    }
}