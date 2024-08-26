/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Explore
{
    public partial class UI_FightRewardPreviewUI : GLabel
    {
        public Controller m_ad;
        public GGraph m_mask;
        public GList m_list1;
        public GGroup m_rewardGroup1;
        public GList m_list2;
        public GGroup m_rewardGroup2;
        public const string URL = "ui://ow12is1hm3fd2e";

        public static UI_FightRewardPreviewUI CreateInstance()
        {
            return (UI_FightRewardPreviewUI)UIPackage.CreateObject("Explore", "FightRewardPreviewUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_ad = GetControllerAt(0);
            m_mask = (GGraph)GetChildAt(0);
            m_list1 = (GList)GetChildAt(5);
            m_rewardGroup1 = (GGroup)GetChildAt(6);
            m_list2 = (GList)GetChildAt(11);
            m_rewardGroup2 = (GGroup)GetChildAt(12);
        }
    }
}