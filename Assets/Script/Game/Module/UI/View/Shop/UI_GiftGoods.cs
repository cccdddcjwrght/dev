/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Shop
{
    public partial class UI_GiftGoods : GComponent
    {
        public UI_BigGoods m_body;
        public const string URL = "ui://aphwhwgne9cj15";

        public static UI_GiftGoods CreateInstance()
        {
            return (UI_GiftGoods)UIPackage.CreateObject("Shop", "GiftGoods");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_body = (UI_BigGoods)GetChildAt(0);
        }
    }
}