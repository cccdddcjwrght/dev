/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Explore
{
    public partial class UI_FightLevelUI : GLabel
    {
        public GLabel m_body;
        public GTextField m_name;
        public GTextField m_fight;
        public GTextField m_grade;
        public GList m_list;
        public GButton m_battleBtn;
        public GLoader m_tip;
        public const string URL = "ui://ow12is1hkp2b1r";

        public static UI_FightLevelUI CreateInstance()
        {
            return (UI_FightLevelUI)UIPackage.CreateObject("Explore", "FightLevelUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_body = (GLabel)GetChildAt(0);
            m_name = (GTextField)GetChildAt(1);
            m_fight = (GTextField)GetChildAt(5);
            m_grade = (GTextField)GetChildAt(8);
            m_list = (GList)GetChildAt(10);
            m_battleBtn = (GButton)GetChildAt(15);
            m_tip = (GLoader)GetChildAt(16);
        }
    }
}