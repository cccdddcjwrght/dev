/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Cookbook
{
    public partial class UI_PropertyUpdate : GLabel
    {
        public Controller m_state;
        public GTextField m_val;
        public GTextField m_next;
        public const string URL = "ui://n2tgmsyur4i1b";

        public static UI_PropertyUpdate CreateInstance()
        {
            return (UI_PropertyUpdate)UIPackage.CreateObject("Cookbook", "PropertyUpdate");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_state = GetControllerAt(0);
            m_val = (GTextField)GetChildAt(1);
            m_next = (GTextField)GetChildAt(3);
        }
    }
}