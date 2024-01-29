/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Buff
{
    public partial class UI_LevelTechUI : GComponent
    {
        public Controller m_completed;
        public GLabel m_body;
        public GList m_BuffList;
        public GTextField m_title;
        public const string URL = "ui://g406runaijal5";

        public static UI_LevelTechUI CreateInstance()
        {
            return (UI_LevelTechUI)UIPackage.CreateObject("Buff", "LevelTechUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_completed = GetControllerAt(0);
            m_body = (GLabel)GetChildAt(0);
            m_BuffList = (GList)GetChildAt(1);
            m_title = (GTextField)GetChildAt(2);
        }
    }
}