/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Pet
{
    public partial class UI_PetEggBtn : GButton
    {
        public Controller m_quality;
        public Controller m_state;
        public Controller m_select;
        public Controller m___redpoint;
        public GButton m_add;
        public GProgressBar m_progress;
        public GTextField m_time;
        public GTextField m_gettips;
        public const string URL = "ui://srlw77oben1x5r";

        public static UI_PetEggBtn CreateInstance()
        {
            return (UI_PetEggBtn)UIPackage.CreateObject("Pet", "PetEggBtn");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_quality = GetControllerAt(0);
            m_state = GetControllerAt(1);
            m_select = GetControllerAt(2);
            m___redpoint = GetControllerAt(3);
            m_add = (GButton)GetChildAt(1);
            m_progress = (GProgressBar)GetChildAt(4);
            m_time = (GTextField)GetChildAt(5);
            m_gettips = (GTextField)GetChildAt(6);
        }
    }
}