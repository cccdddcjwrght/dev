/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Pet
{
    public partial class UI_PetStepBtn : GButton
    {
        public Controller m_step;
        public const string URL = "ui://srlw77obl7ed1k";

        public static UI_PetStepBtn CreateInstance()
        {
            return (UI_PetStepBtn)UIPackage.CreateObject("Pet", "PetStepBtn");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_step = GetControllerAt(0);
        }
    }
}