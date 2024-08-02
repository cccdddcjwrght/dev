/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Pet
{
    public partial class UI_PetModel : GLabel
    {
        public Controller m_quality;
        public Controller m_type;
        public UI_SimplePetModel m_model;
        public GLoader m_tbg;
        public GButton m_free;
        public const string URL = "ui://srlw77obl7ed15";

        public static UI_PetModel CreateInstance()
        {
            return (UI_PetModel)UIPackage.CreateObject("Pet", "PetModel");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_quality = GetControllerAt(0);
            m_type = GetControllerAt(1);
            m_model = (UI_SimplePetModel)GetChildAt(0);
            m_tbg = (GLoader)GetChildAt(1);
            m_free = (GButton)GetChildAt(3);
        }
    }
}