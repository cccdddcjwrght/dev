/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Pet
{
    public partial class UI_PetUI : GLabel
    {
        public Controller m_tab;
        public Controller m_type;
        public GLabel m_body;
        public UI_PetInfo m_pet;
        public UI_PetEgg m_egg;
        public GList m_list;
        public GGraph m___effect;
        public GGraph m___effect2;
        public const string URL = "ui://srlw77obl7ed12";

        public static UI_PetUI CreateInstance()
        {
            return (UI_PetUI)UIPackage.CreateObject("Pet", "PetUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_tab = GetControllerAt(0);
            m_type = GetControllerAt(1);
            m_body = (GLabel)GetChildAt(0);
            m_pet = (UI_PetInfo)GetChildAt(2);
            m_egg = (UI_PetEgg)GetChildAt(3);
            m_list = (GList)GetChildAt(6);
            m___effect = (GGraph)GetChildAt(7);
            m___effect2 = (GGraph)GetChildAt(8);
        }
    }
}