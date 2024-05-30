/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Cookbook
{
    public partial class UI_BookItem : GButton
    {
        public Controller m___redpoint;
        public GLabel m_body;
        public const string URL = "ui://n2tgmsyur4i18";

        public static UI_BookItem CreateInstance()
        {
            return (UI_BookItem)UIPackage.CreateObject("Cookbook", "BookItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m___redpoint = GetControllerAt(0);
            m_body = (GLabel)GetChildAt(0);
        }
    }
}