/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Cookbook
{
    public partial class UI_BookCustomer : GButton
    {
        public Controller m_showTitle;
        public Controller m_state;
        public GLoader m_bg;
        public UI_Customer m_body;
        public const string URL = "ui://n2tgmsyutkwvq";

        public static UI_BookCustomer CreateInstance()
        {
            return (UI_BookCustomer)UIPackage.CreateObject("Cookbook", "BookCustomer");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_showTitle = GetControllerAt(0);
            m_state = GetControllerAt(1);
            m_bg = (GLoader)GetChildAt(0);
            m_body = (UI_Customer)GetChildAt(1);
        }
    }
}