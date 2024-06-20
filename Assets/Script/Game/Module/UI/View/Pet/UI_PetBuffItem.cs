/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Pet
{
    public partial class UI_PetBuffItem : GLabel
    {
        public Controller m_type;
        public Controller m_addstate;
        public GTextField m_add;
        public const string URL = "ui://srlw77obl7ed1e";

        public static UI_PetBuffItem CreateInstance()
        {
            return (UI_PetBuffItem)UIPackage.CreateObject("Pet", "PetBuffItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_type = GetControllerAt(0);
            m_addstate = GetControllerAt(1);
            m_add = (GTextField)GetChildAt(2);
        }
    }
}