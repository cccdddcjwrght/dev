/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.TomorrowGift
{
    public partial class UI_CommomGift : GComponent
    {
        public GList m_GiftItems;
        public const string URL = "ui://7crbg35hk51rc";

        public static UI_CommomGift CreateInstance()
        {
            return (UI_CommomGift)UIPackage.CreateObject("TomorrowGift", "CommomGift");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_GiftItems = (GList)GetChildAt(1);
        }
    }
}