/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Reputation
{
    public partial class UI_TotalBoostUI : GLabel
    {
        public GLabel m_body;
        public GTextField m_worker;
        public GTextField m_customer;
        public GTextField m_totalNum;
        public GList m_list;
        public GGroup m_totalGroup;
        public const string URL = "ui://vnok3a30pynwb";

        public static UI_TotalBoostUI CreateInstance()
        {
            return (UI_TotalBoostUI)UIPackage.CreateObject("Reputation", "TotalBoostUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_body = (GLabel)GetChildAt(0);
            m_worker = (GTextField)GetChildAt(2);
            m_customer = (GTextField)GetChildAt(4);
            m_totalNum = (GTextField)GetChildAt(7);
            m_list = (GList)GetChildAt(9);
            m_totalGroup = (GGroup)GetChildAt(10);
        }
    }
}