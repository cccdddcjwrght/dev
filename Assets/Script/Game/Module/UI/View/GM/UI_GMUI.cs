/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.GM
{
    public partial class UI_GMUI : GComponent
    {
        public Controller m_c1;
        public Controller m___disbalelocal;
        public UI_C1 m_btnGM;
        public GList m_list;
        public UI_C2 m_excute;
        public GTextField m_lblLevel;
        public const string URL = "ui://wdfxjeelutmx0";

        public static UI_GMUI CreateInstance()
        {
            return (UI_GMUI)UIPackage.CreateObject("GM", "GMUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_c1 = GetControllerAt(0);
            m___disbalelocal = GetControllerAt(1);
            m_btnGM = (UI_C1)GetChildAt(0);
            m_list = (GList)GetChildAt(3);
            m_excute = (UI_C2)GetChildAt(4);
            m_lblLevel = (GTextField)GetChildAt(7);
        }
    }
}