/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Reputation
{
    public partial class UI_ReputationItem : GComponent
    {
        public Controller m_inforce;
        public Controller m_markState;
        public GLoader m_icon;
        public GTextField m_info;
        public GLoader m_mark;
        public const string URL = "ui://vnok3a309ehb8";

        public static UI_ReputationItem CreateInstance()
        {
            return (UI_ReputationItem)UIPackage.CreateObject("Reputation", "ReputationItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_inforce = GetControllerAt(0);
            m_markState = GetControllerAt(1);
            m_icon = (GLoader)GetChildAt(2);
            m_info = (GTextField)GetChildAt(3);
            m_mark = (GLoader)GetChildAt(4);
        }
    }
}