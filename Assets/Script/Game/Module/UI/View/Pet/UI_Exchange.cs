/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Pet
{
    public partial class UI_Exchange : GComponent
    {
        public Controller m_price1;
        public Controller m_price2;
        public GLabel m_item1;
        public GLabel m_item2;
        public const string URL = "ui://srlw77obl7ed1t";

        public static UI_Exchange CreateInstance()
        {
            return (UI_Exchange)UIPackage.CreateObject("Pet", "Exchange");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_price1 = GetControllerAt(0);
            m_price2 = GetControllerAt(1);
            m_item1 = (GLabel)GetChildAt(0);
            m_item2 = (GLabel)GetChildAt(1);
        }
    }
}