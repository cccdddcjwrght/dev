/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Player
{
    public partial class UI_EqHelpItem : GComponent
    {
        public UI_Equip m_eq1;
        public UI_Equip m_eq2;
        public GList m_mats;
        public const string URL = "ui://cmw7t1elkvvz6t";

        public static UI_EqHelpItem CreateInstance()
        {
            return (UI_EqHelpItem)UIPackage.CreateObject("Player", "EqHelpItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_eq1 = (UI_Equip)GetChildAt(4);
            m_eq2 = (UI_Equip)GetChildAt(5);
            m_mats = (GList)GetChildAt(6);
        }
    }
}