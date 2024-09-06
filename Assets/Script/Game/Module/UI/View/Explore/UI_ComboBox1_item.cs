/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Explore
{
    public partial class UI_ComboBox1_item : GButton
    {
        public Controller m_type;
        public Controller m_quality;
        public Controller m_islock;
        public GTextField m___text;
        public const string URL = "ui://ow12is1hpiyq3c";

        public static UI_ComboBox1_item CreateInstance()
        {
            return (UI_ComboBox1_item)UIPackage.CreateObject("Explore", "ComboBox1_item");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_type = GetControllerAt(1);
            m_quality = GetControllerAt(2);
            m_islock = GetControllerAt(3);
            m___text = (GTextField)GetChildAt(1);
        }
    }
}