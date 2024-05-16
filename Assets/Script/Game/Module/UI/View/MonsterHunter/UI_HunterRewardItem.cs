/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.MonsterHunter
{
    public partial class UI_HunterRewardItem : GComponent
    {
        public Controller m_type;
        public GLoader m_icon;
        public GTextField m_title;
        public Transition m_show;
        public const string URL = "ui://my7wqjw6dfdxlbb";

        public static UI_HunterRewardItem CreateInstance()
        {
            return (UI_HunterRewardItem)UIPackage.CreateObject("MonsterHunter", "HunterRewardItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_type = GetControllerAt(0);
            m_icon = (GLoader)GetChildAt(1);
            m_title = (GTextField)GetChildAt(2);
            m_show = GetTransitionAt(0);
        }
    }
}