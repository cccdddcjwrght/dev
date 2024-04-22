/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Pet
{
    public partial class UI_PetEgg : GComponent
    {
        public Controller m_state;
        public Controller m_select;
        public GButton m_add;
        public GProgressBar m_progress;
        public GTextField m_time;
        public GTextField m_price;
        public GButton m_get1;
        public GButton m_get2;
        public GButton m_get3;
        public const string URL = "ui://srlw77obl7ed1m";

        public static UI_PetEgg CreateInstance()
        {
            return (UI_PetEgg)UIPackage.CreateObject("Pet", "PetEgg");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_state = GetControllerAt(0);
            m_select = GetControllerAt(1);
            m_add = (GButton)GetChildAt(2);
            m_progress = (GProgressBar)GetChildAt(6);
            m_time = (GTextField)GetChildAt(7);
            m_price = (GTextField)GetChildAt(9);
            m_get1 = (GButton)GetChildAt(11);
            m_get2 = (GButton)GetChildAt(13);
            m_get3 = (GButton)GetChildAt(14);
        }
    }
}