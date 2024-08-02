/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Pet
{
    public partial class UI_PetEggItem : GLabel
    {
        public Controller m_quality;
        public Controller m_state;
        public Controller m_notempty;
        public Controller m_hideline;
        public Controller m___redpoint;
        public GTextField m_count;
        public GTextField m_qname;
        public GProgressBar m_progress;
        public GTextField m_time;
        public GButton m_get3;
        public GButton m_get2;
        public GButton m_get1;
        public const string URL = "ui://srlw77oben1x5x";

        public static UI_PetEggItem CreateInstance()
        {
            return (UI_PetEggItem)UIPackage.CreateObject("Pet", "PetEggItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_quality = GetControllerAt(0);
            m_state = GetControllerAt(1);
            m_notempty = GetControllerAt(2);
            m_hideline = GetControllerAt(3);
            m___redpoint = GetControllerAt(4);
            m_count = (GTextField)GetChildAt(4);
            m_qname = (GTextField)GetChildAt(5);
            m_progress = (GProgressBar)GetChildAt(6);
            m_time = (GTextField)GetChildAt(7);
            m_get3 = (GButton)GetChildAt(9);
            m_get2 = (GButton)GetChildAt(10);
            m_get1 = (GButton)GetChildAt(11);
        }
    }
}