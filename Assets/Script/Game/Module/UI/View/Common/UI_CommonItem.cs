/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Common
{
    public partial class UI_CommonItem : GButton
    {
        public Controller m_hidebg;
        public GLoader m_top;
        public GLoader m_bottom;
        public const string URL = "ui://2w8thcm7twfo18";

        public static UI_CommonItem CreateInstance()
        {
            return (UI_CommonItem)UIPackage.CreateObject("Common", "CommonItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_hidebg = GetControllerAt(0);
            m_top = (GLoader)GetChildAt(3);
            m_bottom = (GLoader)GetChildAt(4);
        }
    }
}