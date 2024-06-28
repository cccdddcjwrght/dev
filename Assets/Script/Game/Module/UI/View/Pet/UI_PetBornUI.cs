/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Pet
{
    public partial class UI_PetBornUI : GLabel
    {
        public Controller m_quality;
        public Controller m_type;
        public Controller m_state;
        public GGraph m___effect;
        public UI_SimplePetModel m_model;
        public UI_ChangeProperty m_change;
        public GGraph m_effect2;
        public Transition m_doshow;
        public const string URL = "ui://srlw77obl7ed1w";

        public static UI_PetBornUI CreateInstance()
        {
            return (UI_PetBornUI)UIPackage.CreateObject("Pet", "PetBornUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_quality = GetControllerAt(0);
            m_type = GetControllerAt(1);
            m_state = GetControllerAt(2);
            m___effect = (GGraph)GetChildAt(2);
            m_model = (UI_SimplePetModel)GetChildAt(8);
            m_change = (UI_ChangeProperty)GetChildAt(9);
            m_effect2 = (GGraph)GetChildAt(12);
            m_doshow = GetTransitionAt(0);
        }
    }
}