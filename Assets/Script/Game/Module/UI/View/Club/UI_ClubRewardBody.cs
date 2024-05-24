/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Club
{
    public partial class UI_ClubRewardBody : GLabel
    {
        public GList m_list;
        public GImage m_barbg;
        public GImage m_bar;
        public GList m_topList;
        public GGroup m_top;
        public GGroup m_content;
        public const string URL = "ui://kgizakqqlu5m1w";

        public static UI_ClubRewardBody CreateInstance()
        {
            return (UI_ClubRewardBody)UIPackage.CreateObject("Club", "ClubRewardBody");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_list = (GList)GetChildAt(4);
            m_barbg = (GImage)GetChildAt(6);
            m_bar = (GImage)GetChildAt(7);
            m_topList = (GList)GetChildAt(14);
            m_top = (GGroup)GetChildAt(15);
            m_content = (GGroup)GetChildAt(16);
        }
    }
}