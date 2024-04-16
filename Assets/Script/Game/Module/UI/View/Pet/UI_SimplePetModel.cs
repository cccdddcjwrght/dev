/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Pet
{
    public partial class UI_SimplePetModel : GComponent
    {
        public GGraph m_holder;
        public const string URL = "ui://srlw77obl7ed1r";

        public static UI_SimplePetModel CreateInstance()
        {
            return (UI_SimplePetModel)UIPackage.CreateObject("Pet", "SimplePetModel");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_holder = (GGraph)GetChildAt(1);
        }
    }
}