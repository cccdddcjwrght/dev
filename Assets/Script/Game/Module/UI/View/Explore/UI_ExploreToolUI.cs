/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Explore
{
    public partial class UI_ExploreToolUI : GLabel
    {
        public Controller m_type;
        public GLabel m_body;
        public GTextField m_level;
        public GTextField m_desc;
        public GTextField m_condition;
        public GList m_list;
        public GButton m_click;
        public GButton m_diamondbtn;
        public GButton m_adbtn;
        public GTextField m_time;
        public const string URL = "ui://ow12is1hpm5b1l";

        public static UI_ExploreToolUI CreateInstance()
        {
            return (UI_ExploreToolUI)UIPackage.CreateObject("Explore", "ExploreToolUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_type = GetControllerAt(0);
            m_body = (GLabel)GetChildAt(0);
            m_level = (GTextField)GetChildAt(4);
            m_desc = (GTextField)GetChildAt(5);
            m_condition = (GTextField)GetChildAt(6);
            m_list = (GList)GetChildAt(10);
            m_click = (GButton)GetChildAt(11);
            m_diamondbtn = (GButton)GetChildAt(12);
            m_adbtn = (GButton)GetChildAt(13);
            m_time = (GTextField)GetChildAt(14);
        }
    }
}