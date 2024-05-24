/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Club
{
    public partial class UI_ClubCreateUI : GComponent
    {
        public GLabel m_body;
        public UI_ClubIcon m_clubIcon;
        public UI_IconBtn m_reset;
        public GTextInput m_input;
        public GLoader m_currencyIcon;
        public GTextField m_currencyValue;
        public UI_ClubBtn m_createBtn;
        public GGroup m_content;
        public const string URL = "ui://kgizakqqlu5m1j";

        public static UI_ClubCreateUI CreateInstance()
        {
            return (UI_ClubCreateUI)UIPackage.CreateObject("Club", "ClubCreateUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_body = (GLabel)GetChildAt(0);
            m_clubIcon = (UI_ClubIcon)GetChildAt(1);
            m_reset = (UI_IconBtn)GetChildAt(2);
            m_input = (GTextInput)GetChildAt(5);
            m_currencyIcon = (GLoader)GetChildAt(6);
            m_currencyValue = (GTextField)GetChildAt(7);
            m_createBtn = (UI_ClubBtn)GetChildAt(8);
            m_content = (GGroup)GetChildAt(9);
        }
    }
}