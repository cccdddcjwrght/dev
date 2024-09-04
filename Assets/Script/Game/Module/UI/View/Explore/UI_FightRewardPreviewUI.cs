/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Explore
{
    public partial class UI_FightRewardPreviewUI : GLabel
    {
        public GLabel m_body;
        public GList m_list;
        public GGroup m_content;
        public const string URL = "ui://ow12is1hm3fd2e";

        public static UI_FightRewardPreviewUI CreateInstance()
        {
            return (UI_FightRewardPreviewUI)UIPackage.CreateObject("Explore", "FightRewardPreviewUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_body = (GLabel)GetChildAt(0);
            m_list = (GList)GetChildAt(2);
            m_content = (GGroup)GetChildAt(3);
        }
    }
}