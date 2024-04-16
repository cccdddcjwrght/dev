/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Pet
{
    public partial class UI_PetStep : GComponent
    {
        public Controller m_step;
        public Controller m_reset;
        public Controller m_type;
        public UI_PetStepBtn m_step1;
        public UI_PetStepBtn m_step2;
        public UI_PetStepBtn m_step3;
        public const string URL = "ui://srlw77obl7ed1p";

        public static UI_PetStep CreateInstance()
        {
            return (UI_PetStep)UIPackage.CreateObject("Pet", "PetStep");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_step = GetControllerAt(0);
            m_reset = GetControllerAt(1);
            m_type = GetControllerAt(2);
            m_step1 = (UI_PetStepBtn)GetChildAt(1);
            m_step2 = (UI_PetStepBtn)GetChildAt(2);
            m_step3 = (UI_PetStepBtn)GetChildAt(3);
        }
    }
}