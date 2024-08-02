/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Pet
{
    public partial class UI_PetEggPanel : GComponent
    {
        public GList m_egglist;
        public const string URL = "ui://srlw77oben1x5z";

        public static UI_PetEggPanel CreateInstance()
        {
            return (UI_PetEggPanel)UIPackage.CreateObject("Pet", "PetEggPanel");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_egglist = (GList)GetChildAt(2);
        }
    }
}