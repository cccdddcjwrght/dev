/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Club
{
    public partial class UI_ClubTaskUI : GComponent
    {
        public GLabel m_body;
        public GLoader m_currencyIcon;
        public GTextField m_value;
        public GList m_list;
        public GTextField m_addValue;
        public GGroup m_content;
        public Transition m_play;
        public const string URL = "ui://kgizakqqrllf2f";

        public static UI_ClubTaskUI CreateInstance()
        {
            return (UI_ClubTaskUI)UIPackage.CreateObject("Club", "ClubTaskUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_body = (GLabel)GetChildAt(0);
            m_currencyIcon = (GLoader)GetChildAt(2);
            m_value = (GTextField)GetChildAt(3);
            m_list = (GList)GetChildAt(5);
            m_addValue = (GTextField)GetChildAt(6);
            m_content = (GGroup)GetChildAt(7);
            m_play = GetTransitionAt(0);
        }
    }
}