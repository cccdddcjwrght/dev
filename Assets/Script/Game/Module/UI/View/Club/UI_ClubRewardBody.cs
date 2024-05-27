/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Club
{
    public partial class UI_ClubRewardBody : GLabel
    {
        public Controller m_top;
        public GList m_list;
        public GImage m_barbg;
        public GImage m_bar;
        public GButton m_reddot;
        public GList m_topList;
        public GGraph m_mask;
        public GGroup m_top_2;
        public GGroup m_content;
        public const string URL = "ui://kgizakqqlu5m1w";

        public static UI_ClubRewardBody CreateInstance()
        {
            return (UI_ClubRewardBody)UIPackage.CreateObject("Club", "ClubRewardBody");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_top = GetControllerAt(0);
            m_list = (GList)GetChildAt(4);
            m_barbg = (GImage)GetChildAt(6);
            m_bar = (GImage)GetChildAt(7);
            m_reddot = (GButton)GetChildAt(15);
            m_topList = (GList)GetChildAt(16);
            m_mask = (GGraph)GetChildAt(17);
            m_top_2 = (GGroup)GetChildAt(19);
            m_content = (GGroup)GetChildAt(20);
        }
    }
}