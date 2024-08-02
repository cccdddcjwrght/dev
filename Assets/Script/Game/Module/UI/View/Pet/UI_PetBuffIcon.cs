/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Pet
{
    public partial class UI_PetBuffIcon : GButton
    {
        public Controller m_quality;
        public Controller m_type;
        public const string URL = "ui://srlw77oben1x5q";

        public static UI_PetBuffIcon CreateInstance()
        {
            return (UI_PetBuffIcon)UIPackage.CreateObject("Pet", "PetBuffIcon");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_quality = GetControllerAt(0);
            m_type = GetControllerAt(1);
        }
    }
}