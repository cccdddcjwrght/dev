/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Pet
{
    public partial class UI_PetMedalList : GComponent
    {
        public Controller m_type;
        public Controller m_price;
        public Controller m_price2;
        public GButton m_c1;
        public GButton m_c2;
        public GButton m_c3;
        public const string URL = "ui://srlw77ob835x4c";

        public static UI_PetMedalList CreateInstance()
        {
            return (UI_PetMedalList)UIPackage.CreateObject("Pet", "PetMedalList");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_type = GetControllerAt(0);
            m_price = GetControllerAt(1);
            m_price2 = GetControllerAt(2);
            m_c1 = (GButton)GetChildAt(0);
            m_c2 = (GButton)GetChildAt(1);
            m_c3 = (GButton)GetChildAt(2);
        }
    }
}