/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Explore
{
    public partial class UI_ExploreSelectBtn : GComboBox
    {
        public Controller m_type;
        public Controller m_quality;
        public const string URL = "ui://ow12is1hpiyq3b";

        public static UI_ExploreSelectBtn CreateInstance()
        {
            return (UI_ExploreSelectBtn)UIPackage.CreateObject("Explore", "ExploreSelectBtn");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_type = GetControllerAt(1);
            m_quality = GetControllerAt(2);
        }
    }
}