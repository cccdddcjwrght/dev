/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Cookbook
{
    public partial class UI_BookItem : GButton
    {
        public Controller m_state;
        public Controller m_hidelv;
        public Controller m_maxlv;
        public Controller m_selected;
        public GLabel m_body;
        public GTextField m_level;
        public const string URL = "ui://n2tgmsyur4i18";

        public static UI_BookItem CreateInstance()
        {
            return (UI_BookItem)UIPackage.CreateObject("Cookbook", "BookItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_state = GetControllerAt(0);
            m_hidelv = GetControllerAt(1);
            m_maxlv = GetControllerAt(2);
            m_selected = GetControllerAt(3);
            m_body = (GLabel)GetChildAt(0);
            m_level = (GTextField)GetChildAt(4);
        }
    }
}