/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Player
{
    public partial class UI_SuitUI : GLabel
    {
        public Controller m_funcType;
        public Controller m_part;
        public Controller m_quality;
        public Controller m_flag;
        public GLabel m_body;
        public GButton m_click;
        public GList m_list;
        public GTextField m_tips;
        public const string URL = "ui://cmw7t1elwaj649";

        public static UI_SuitUI CreateInstance()
        {
            return (UI_SuitUI)UIPackage.CreateObject("Player", "SuitUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_funcType = GetControllerAt(0);
            m_part = GetControllerAt(1);
            m_quality = GetControllerAt(2);
            m_flag = GetControllerAt(3);
            m_body = (GLabel)GetChildAt(0);
            m_click = (GButton)GetChildAt(7);
            m_list = (GList)GetChildAt(9);
            m_tips = (GTextField)GetChildAt(10);
        }
    }
}