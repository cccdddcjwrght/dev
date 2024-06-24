/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Cookbook
{
    public partial class UI_CookbookUI : GLabel
    {
        public Controller m_tabs;
        public GLabel m_body;
        public GList m_list;
        public GList m_listCustomer;
        public const string URL = "ui://n2tgmsyur4i16";

        public static UI_CookbookUI CreateInstance()
        {
            return (UI_CookbookUI)UIPackage.CreateObject("Cookbook", "CookbookUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_tabs = GetControllerAt(0);
            m_body = (GLabel)GetChildAt(0);
            m_list = (GList)GetChildAt(5);
            m_listCustomer = (GList)GetChildAt(6);
        }
    }
}