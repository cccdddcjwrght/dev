/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.MonsterHunter
{
    public partial class UI_PanelBody : GComponent
    {
        public UI_HunterBullet m_item0_x;
        public UI_HunterBullet m_item1_x;
        public UI_HunterBullet m_item2_x;
        public UI_HunterBullet m_item3_x;
        public UI_HunterBullet m_item4_x;
        public UI_HunterBullet m_item5_x;
        public UI_HunterBullet m_item6_x;
        public UI_HunterBullet m_item7_x;
        public UI_HunterBullet m_item8_x;
        public UI_HunterBullet m_item9_x;
        public UI_HunterBullet m_item10_x;
        public UI_HunterBullet m_item11_x;
        public const string URL = "ui://my7wqjw6twfo18";

        public static UI_PanelBody CreateInstance()
        {
            return (UI_PanelBody)UIPackage.CreateObject("MonsterHunter", "PanelBody");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_item0_x = (UI_HunterBullet)GetChildAt(0);
            m_item1_x = (UI_HunterBullet)GetChildAt(1);
            m_item2_x = (UI_HunterBullet)GetChildAt(2);
            m_item3_x = (UI_HunterBullet)GetChildAt(3);
            m_item4_x = (UI_HunterBullet)GetChildAt(4);
            m_item5_x = (UI_HunterBullet)GetChildAt(5);
            m_item6_x = (UI_HunterBullet)GetChildAt(6);
            m_item7_x = (UI_HunterBullet)GetChildAt(7);
            m_item8_x = (UI_HunterBullet)GetChildAt(8);
            m_item9_x = (UI_HunterBullet)GetChildAt(9);
            m_item10_x = (UI_HunterBullet)GetChildAt(10);
            m_item11_x = (UI_HunterBullet)GetChildAt(11);
        }
    }
}