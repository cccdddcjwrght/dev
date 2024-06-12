/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Pet
{
    public partial class UI_PetBornUI : GLabel
    {
        public Controller m_quality;
        public GGraph m___effect;
        public UI_SimplePetModel m_model;
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
            m___effect = (GGraph)GetChildAt(3);
            m_model = (UI_SimplePetModel)GetChildAt(8);
            m_doshow = GetTransitionAt(0);
        }
    }
}