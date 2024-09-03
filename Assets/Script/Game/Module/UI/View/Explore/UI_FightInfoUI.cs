/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Explore
{
    public partial class UI_FightInfoUI : GLabel
    {
        public GLabel m_body;
        public GList m_attr;
        public const string URL = "ui://ow12is1hpm5b1g";

        public static UI_FightInfoUI CreateInstance()
        {
            return (UI_FightInfoUI)UIPackage.CreateObject("Explore", "FightInfoUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_body = (GLabel)GetChildAt(0);
            m_attr = (GList)GetChildAt(2);
        }
    }
}