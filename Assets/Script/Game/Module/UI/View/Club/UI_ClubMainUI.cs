/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Club
{
    public partial class UI_ClubMainUI : GComponent
    {
        public GList m_bodyList;
        public GLabel m_body;
        public GTextField m_time;
        public GLoader m_currencyIcon;
        public GTextField m_value;
        public UI_ClubItem m_clubItem;
        public UI_IconBtn m_memberBtn;
        public UI_IconBtn m_taskBtn;
        public GGroup m_content;
        public const string URL = "ui://kgizakqqlu5m1n";

        public static UI_ClubMainUI CreateInstance()
        {
            return (UI_ClubMainUI)UIPackage.CreateObject("Club", "ClubMainUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_bodyList = (GList)GetChildAt(0);
            m_body = (GLabel)GetChildAt(1);
            m_time = (GTextField)GetChildAt(4);
            m_currencyIcon = (GLoader)GetChildAt(6);
            m_value = (GTextField)GetChildAt(7);
            m_clubItem = (UI_ClubItem)GetChildAt(8);
            m_memberBtn = (UI_IconBtn)GetChildAt(9);
            m_taskBtn = (UI_IconBtn)GetChildAt(10);
            m_content = (GGroup)GetChildAt(11);
        }
    }
}