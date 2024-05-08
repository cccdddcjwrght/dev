/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Player
{
    public partial class UI_PropertyInfoUI : GLabel
    {
        public GGraph m_mask;
        public GLabel m_body;
        public GList m_list;
        public const string URL = "ui://cmw7t1elhrqh1p";

        public static UI_PropertyInfoUI CreateInstance()
        {
            return (UI_PropertyInfoUI)UIPackage.CreateObject("Player", "PropertyInfoUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_mask = (GGraph)GetChildAt(0);
            m_body = (GLabel)GetChildAt(1);
            m_list = (GList)GetChildAt(5);
        }
    }
}