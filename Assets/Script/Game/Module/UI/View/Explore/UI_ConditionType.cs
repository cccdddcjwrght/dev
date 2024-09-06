/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Explore
{
    public partial class UI_ConditionType : GButton
    {
        public Controller m_type;
        public const string URL = "ui://ow12is1hpiyq3f";

        public static UI_ConditionType CreateInstance()
        {
            return (UI_ConditionType)UIPackage.CreateObject("Explore", "ConditionType");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_type = GetControllerAt(1);
        }
    }
}