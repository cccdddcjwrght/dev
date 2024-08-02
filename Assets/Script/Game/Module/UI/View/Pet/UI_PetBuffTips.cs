/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Pet
{
    public partial class UI_PetBuffTips : GLabel
    {
        public GImage m_tips;
        public const string URL = "ui://srlw77oben1x60";

        public static UI_PetBuffTips CreateInstance()
        {
            return (UI_PetBuffTips)UIPackage.CreateObject("Pet", "PetBuffTips");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_tips = (GImage)GetChildAt(0);
        }
    }
}