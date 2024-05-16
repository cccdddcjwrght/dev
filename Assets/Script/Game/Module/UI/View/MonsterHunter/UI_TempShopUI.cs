/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.MonsterHunter
{
    public partial class UI_TempShopUI : GLabel
    {
        public GLabel m_body;
        public GList m_list;
        public const string URL = "ui://my7wqjw6dfdxlbd";

        public static UI_TempShopUI CreateInstance()
        {
            return (UI_TempShopUI)UIPackage.CreateObject("MonsterHunter", "TempShopUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_body = (GLabel)GetChildAt(0);
            m_list = (GList)GetChildAt(1);
        }
    }
}