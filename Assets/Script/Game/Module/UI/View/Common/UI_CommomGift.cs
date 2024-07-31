/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Common
{
    public partial class UI_CommomGift : GComponent
    {
        public GList m_GiftItems;
        public const string URL = "ui://2w8thcm7s86pj";

        public static UI_CommomGift CreateInstance()
        {
            return (UI_CommomGift)UIPackage.CreateObject("Common", "CommomGift");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_GiftItems = (GList)GetChildAt(1);
        }
    }
}