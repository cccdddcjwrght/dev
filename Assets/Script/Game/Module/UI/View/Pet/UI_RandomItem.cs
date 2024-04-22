/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Pet
{
    public partial class UI_RandomItem : GButton
    {
        public Controller m_select;
        public GGraph m___effect;
        public Transition m_t0;
        public Transition m_t1;
        public const string URL = "ui://srlw77ob835x4f";

        public static UI_RandomItem CreateInstance()
        {
            return (UI_RandomItem)UIPackage.CreateObject("Pet", "RandomItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_select = GetControllerAt(0);
            m___effect = (GGraph)GetChildAt(0);
            m_t0 = GetTransitionAt(0);
            m_t1 = GetTransitionAt(1);
        }
    }
}