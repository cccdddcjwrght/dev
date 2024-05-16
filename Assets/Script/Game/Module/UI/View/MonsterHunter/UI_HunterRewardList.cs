/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.MonsterHunter
{
    public partial class UI_HunterRewardList : GComponent
    {
        public Controller m_size;
        public GList m_list;
        public const string URL = "ui://my7wqjw6twfo1b";

        public static UI_HunterRewardList CreateInstance()
        {
            return (UI_HunterRewardList)UIPackage.CreateObject("MonsterHunter", "HunterRewardList");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_size = GetControllerAt(0);
            m_list = (GList)GetChildAt(1);
        }
    }
}