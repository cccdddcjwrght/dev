/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Club
{
    public partial class UI_ClubReward : GComponent
    {
        public Controller m_state;
        public GLoader m_icon;
        public GTextField m_value;
        public GButton m_reddot;
        public GGraph m_mask;
        public const string URL = "ui://kgizakqqlu5m2a";

        public static UI_ClubReward CreateInstance()
        {
            return (UI_ClubReward)UIPackage.CreateObject("Club", "ClubReward");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_state = GetControllerAt(0);
            m_icon = (GLoader)GetChildAt(2);
            m_value = (GTextField)GetChildAt(3);
            m_reddot = (GButton)GetChildAt(4);
            m_mask = (GGraph)GetChildAt(5);
        }
    }
}