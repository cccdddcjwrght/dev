/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Explore
{
    public partial class UI_FightWinUI : GLabel
    {
        public GButton m_getBtn;
        public GButton m_adBtn;
        public GList m_rewardList1;
        public GList m_rewardList2;
        public const string URL = "ui://ow12is1hkp2b1u";

        public static UI_FightWinUI CreateInstance()
        {
            return (UI_FightWinUI)UIPackage.CreateObject("Explore", "FightWinUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_getBtn = (GButton)GetChildAt(8);
            m_adBtn = (GButton)GetChildAt(9);
            m_rewardList1 = (GList)GetChildAt(10);
            m_rewardList2 = (GList)GetChildAt(11);
        }
    }
}