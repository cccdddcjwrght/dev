/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Cookbook
{
    public partial class UI_PropertyUpdateIcon : GLabel
    {
        public Controller m_state;
        public GTextField m_val2;
        public GTextField m_val;
        public GTextField m_next;
        public const string URL = "ui://n2tgmsyuq90gc";

        public static UI_PropertyUpdateIcon CreateInstance()
        {
            return (UI_PropertyUpdateIcon)UIPackage.CreateObject("Cookbook", "PropertyUpdateIcon");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_state = GetControllerAt(0);
            m_val2 = (GTextField)GetChildAt(0);
            m_val = (GTextField)GetChildAt(1);
            m_next = (GTextField)GetChildAt(3);
        }
    }
}