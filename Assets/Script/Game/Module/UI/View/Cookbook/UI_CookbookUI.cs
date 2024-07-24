/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Cookbook
{
    public partial class UI_CookbookUI : GLabel
    {
        public Controller m_tabs;
        public UI_CollectBg m_body;
        public UI_CollectTab m_waiter;
        public UI_CollectTab m_cooker;
        public GGroup m_tabs_2;
        public GList m_list;
        public GList m_listCustomer;
        public GList m_listworker;
        public GList m_addpropertys;
        public GGraph m_effectholder;
        public Transition m_doshow;
        public const string URL = "ui://n2tgmsyur4i16";

        public static UI_CookbookUI CreateInstance()
        {
            return (UI_CookbookUI)UIPackage.CreateObject("Cookbook", "CookbookUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_tabs = GetControllerAt(0);
            m_body = (UI_CollectBg)GetChildAt(0);
            m_waiter = (UI_CollectTab)GetChildAt(3);
            m_cooker = (UI_CollectTab)GetChildAt(4);
            m_tabs_2 = (GGroup)GetChildAt(5);
            m_list = (GList)GetChildAt(7);
            m_listCustomer = (GList)GetChildAt(8);
            m_listworker = (GList)GetChildAt(9);
            m_addpropertys = (GList)GetChildAt(10);
            m_effectholder = (GGraph)GetChildAt(12);
            m_doshow = GetTransitionAt(0);
        }
    }
}