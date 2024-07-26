/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Reputation
{
    public partial class UI_BigLuckShow : GLabel
    {
        public GGraph m_bg;
        public GList m_list;
        public GGraph m_effect;
        public Transition m_show;
        public const string URL = "ui://vnok3a30khnp1f";

        public static UI_BigLuckShow CreateInstance()
        {
            return (UI_BigLuckShow)UIPackage.CreateObject("Reputation", "BigLuckShow");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_bg = (GGraph)GetChildAt(0);
            m_list = (GList)GetChildAt(2);
            m_effect = (GGraph)GetChildAt(3);
            m_show = GetTransitionAt(0);
        }
    }
}