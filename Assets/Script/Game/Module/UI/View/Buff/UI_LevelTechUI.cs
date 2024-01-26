/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Buff
{
    public partial class UI_LevelTechUI : GComponent
    {
        public GComponent m_body;
        public GList m_BuffList;
        public const string URL = "ui://g406runaijal5";

        public static UI_LevelTechUI CreateInstance()
        {
            return (UI_LevelTechUI)UIPackage.CreateObject("Buff", "LevelTechUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_body = (GComponent)GetChildAt(0);
            m_BuffList = (GList)GetChildAt(1);
        }
    }
}