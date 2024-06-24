/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Cookbook
{
    public partial class UI_BtnIconItem : GButton
    {
        public GLabel m_item;
        public const string URL = "ui://n2tgmsyusj1y13";

        public static UI_BtnIconItem CreateInstance()
        {
            return (UI_BtnIconItem)UIPackage.CreateObject("Cookbook", "BtnIconItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_item = (GLabel)GetChildAt(0);
        }
    }
}