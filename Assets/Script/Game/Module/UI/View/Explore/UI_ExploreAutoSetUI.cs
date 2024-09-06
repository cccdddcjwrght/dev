/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Explore
{
    public partial class UI_ExploreAutoSetUI : GLabel
    {
        public Controller m_cdtype;
        public Controller m_quality;
        public Controller m_comparepower;
        public GLabel m_body;
        public GList m_atts;
        public UI_ExploreSelectBtn m_costselect;
        public UI_ExploreSelectBtn m_qualityselect;
        public UI_ConditionType m_cdtype_2;
        public UI_CheckBox m_toggle;
        public GButton m_click;
        public const string URL = "ui://ow12is1hpiyq30";

        public static UI_ExploreAutoSetUI CreateInstance()
        {
            return (UI_ExploreAutoSetUI)UIPackage.CreateObject("Explore", "ExploreAutoSetUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_cdtype = GetControllerAt(0);
            m_quality = GetControllerAt(1);
            m_comparepower = GetControllerAt(2);
            m_body = (GLabel)GetChildAt(0);
            m_atts = (GList)GetChildAt(8);
            m_costselect = (UI_ExploreSelectBtn)GetChildAt(9);
            m_qualityselect = (UI_ExploreSelectBtn)GetChildAt(10);
            m_cdtype_2 = (UI_ConditionType)GetChildAt(11);
            m_toggle = (UI_CheckBox)GetChildAt(12);
            m_click = (GButton)GetChildAt(13);
        }
    }
}