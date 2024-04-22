/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Pet
{
    public partial class UI_PetInfo : GComponent
    {
        public Controller m_quality;
        public Controller m_step;
        public Controller m_lock;
        public UI_PetModel m_model;
        public GList m_bufflist;
        public const string URL = "ui://srlw77obl7ed1j";

        public static UI_PetInfo CreateInstance()
        {
            return (UI_PetInfo)UIPackage.CreateObject("Pet", "PetInfo");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_quality = GetControllerAt(0);
            m_step = GetControllerAt(1);
            m_lock = GetControllerAt(2);
            m_model = (UI_PetModel)GetChildAt(1);
            m_bufflist = (GList)GetChildAt(3);
        }
    }
}