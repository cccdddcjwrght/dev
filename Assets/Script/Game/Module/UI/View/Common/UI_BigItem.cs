/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Common
{
    public partial class UI_BigItem : GLabel
    {
        public UI_CommonItem m_body;
        public const string URL = "ui://2w8thcm7gkcg3li0";

        public static UI_BigItem CreateInstance()
        {
            return (UI_BigItem)UIPackage.CreateObject("Common", "BigItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_body = (UI_CommonItem)GetChildAt(0);
        }
    }
}