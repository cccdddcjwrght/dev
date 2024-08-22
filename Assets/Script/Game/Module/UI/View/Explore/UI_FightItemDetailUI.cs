/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Explore
{
    public partial class UI_FightItemDetailUI : GLabel
    {
        public GLabel m_body;
        public GList m_list;
        public const string URL = "ui://ow12is1hkp2b22";

        public static UI_FightItemDetailUI CreateInstance()
        {
            return (UI_FightItemDetailUI)UIPackage.CreateObject("Explore", "FightItemDetailUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_body = (GLabel)GetChildAt(0);
            m_list = (GList)GetChildAt(1);
        }
    }
}