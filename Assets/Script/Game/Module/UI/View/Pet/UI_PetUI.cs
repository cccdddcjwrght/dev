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
        public GGraph m_middle;
        public GGraph m_bottom;
        public UI_PetInfo m_pet;
        public GButton m_click;
        public UI_PetEggBtn m_egg;
        public GList m_list;
        public UI_PetEggPanel m_eggpanel;
        public UI_PetBuffTips m_tips;
        public GGraph m___effect;
        public GGraph m___effect2;
        public const string URL = "ui://srlw77oben1x4z";

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
            m_middle = (GGraph)GetChildAt(1);
            m_bottom = (GGraph)GetChildAt(2);
            m_pet = (UI_PetInfo)GetChildAt(4);
            m_click = (GButton)GetChildAt(5);
            m_egg = (UI_PetEggBtn)GetChildAt(6);
            m_list = (GList)GetChildAt(9);
            m_eggpanel = (UI_PetEggPanel)GetChildAt(11);
            m_tips = (UI_PetBuffTips)GetChildAt(13);
            m___effect = (GGraph)GetChildAt(14);
            m___effect2 = (GGraph)GetChildAt(15);
        }
    }
}