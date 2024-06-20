/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Pet
{
    public partial class UI_PetIcon : GButton
    {
        public Controller m_quality;
        public Controller m_selected;
        public Controller m___redpoint;
        public GLoader m_bg;
        public GTextField m_count;
        public const string URL = "ui://srlw77obl7ed1l";

        public static UI_PetIcon CreateInstance()
        {
            return (UI_PetIcon)UIPackage.CreateObject("Pet", "PetIcon");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_quality = GetControllerAt(0);
            m_selected = GetControllerAt(1);
            m___redpoint = GetControllerAt(2);
            m_bg = (GLoader)GetChildAt(0);
            m_count = (GTextField)GetChildAt(3);
        }
    }
}