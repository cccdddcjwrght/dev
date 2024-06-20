/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Pet
{
    public partial class UI_ChangeProperty : GLabel
    {
        public Controller m_type;
        public GTextField m_tips;
        public GTextField m_level;
        public const string URL = "ui://srlw77obp8z44p";

        public static UI_ChangeProperty CreateInstance()
        {
            return (UI_ChangeProperty)UIPackage.CreateObject("Pet", "ChangeProperty");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_type = GetControllerAt(0);
            m_tips = (GTextField)GetChildAt(0);
            m_level = (GTextField)GetChildAt(4);
        }
    }
}