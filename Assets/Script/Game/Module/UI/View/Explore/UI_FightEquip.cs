/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Explore
{
    public partial class UI_FightEquip : GButton
    {
        public Controller m_quality;
        public Controller m_strongstate;
        public Controller m_type;
        public Controller m_part;
        public GLoader m_icon2;
        public GLoader m_bg;
        public GTextField m_level;
        public GLoader m_part_2;
        public GImage m_strong;
        public GGraph m___effect;
        public const string URL = "ui://ow12is1hpm5b16";

        public static UI_FightEquip CreateInstance()
        {
            return (UI_FightEquip)UIPackage.CreateObject("Explore", "FightEquip");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_quality = GetControllerAt(0);
            m_strongstate = GetControllerAt(1);
            m_type = GetControllerAt(2);
            m_part = GetControllerAt(3);
            m_icon2 = (GLoader)GetChildAt(1);
            m_bg = (GLoader)GetChildAt(3);
            m_level = (GTextField)GetChildAt(4);
            m_part_2 = (GLoader)GetChildAt(6);
            m_strong = (GImage)GetChildAt(7);
            m___effect = (GGraph)GetChildAt(9);
        }
    }
}