/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Common
{
    public partial class UI_RewardShowUI : GComponent
    {
        public Controller m_type;
        public GTextField m_title;
        public GList m_list;
        public GTextField m_tip;
        public UI_ClickBtn m_click;
        public GGroup m_content;
        public const string URL = "ui://2w8thcm7n8ghv";

        public static UI_RewardShowUI CreateInstance()
        {
            return (UI_RewardShowUI)UIPackage.CreateObject("Common", "RewardShowUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_type = GetControllerAt(0);
            m_title = (GTextField)GetChildAt(3);
            m_list = (GList)GetChildAt(4);
            m_tip = (GTextField)GetChildAt(5);
            m_click = (UI_ClickBtn)GetChildAt(6);
            m_content = (GGroup)GetChildAt(7);
        }
    }
}