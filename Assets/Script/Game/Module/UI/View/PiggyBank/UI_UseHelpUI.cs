/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.PiggyBank
{
    public partial class UI_UseHelpUI : GComponent
    {
        public GGraph m_mask;
        public GTextField m_title;
        public GTextField m_info1;
        public GTextField m_info2;
        public GTextField m_info3;
        public GTextField m_info4;
        public GGroup m_group;
        public const string URL = "ui://k2a58dz5a9b7t";

        public static UI_UseHelpUI CreateInstance()
        {
            return (UI_UseHelpUI)UIPackage.CreateObject("PiggyBank", "UseHelpUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_mask = (GGraph)GetChildAt(0);
            m_title = (GTextField)GetChildAt(1);
            m_info1 = (GTextField)GetChildAt(3);
            m_info2 = (GTextField)GetChildAt(4);
            m_info3 = (GTextField)GetChildAt(7);
            m_info4 = (GTextField)GetChildAt(8);
            m_group = (GGroup)GetChildAt(11);
        }
    }
}