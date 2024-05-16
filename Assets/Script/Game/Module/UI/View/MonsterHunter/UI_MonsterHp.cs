/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.MonsterHunter
{
    public partial class UI_MonsterHp : GProgressBar
    {
        public GButton m_item1;
        public GButton m_item2;
        public const string URL = "ui://my7wqjw6twfot";

        public static UI_MonsterHp CreateInstance()
        {
            return (UI_MonsterHp)UIPackage.CreateObject("MonsterHunter", "MonsterHp");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_item1 = (GButton)GetChildAt(3);
            m_item2 = (GButton)GetChildAt(4);
        }
    }
}