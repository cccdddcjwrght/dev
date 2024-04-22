/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Pet
{
    public partial class UI_PetTipsUI : GLabel
    {
        public Controller m_quality;
        public Controller m_lock;
        public Controller m_step;
        public GLabel m_body;
        public UI_SimplePetModel m_model;
        public GList m_bufflist;
        public GButton m_free;
        public GButton m_click;
        public const string URL = "ui://srlw77obl7ed20";

        public static UI_PetTipsUI CreateInstance()
        {
            return (UI_PetTipsUI)UIPackage.CreateObject("Pet", "PetTipsUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_quality = GetControllerAt(0);
            m_lock = GetControllerAt(1);
            m_step = GetControllerAt(2);
            m_body = (GLabel)GetChildAt(0);
            m_model = (UI_SimplePetModel)GetChildAt(4);
            m_bufflist = (GList)GetChildAt(5);
            m_free = (GButton)GetChildAt(9);
            m_click = (GButton)GetChildAt(10);
        }
    }
}