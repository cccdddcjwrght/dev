/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Reputation
{
    public partial class UI_FragmentUI : GLabel
    {
        public GButton m_btn;
        public GList m_list;
        public GGraph m_effect;
        public GGroup m_content;
        public Transition m_show;
        public const string URL = "ui://vnok3a30jlm51h";

        public static UI_FragmentUI CreateInstance()
        {
            return (UI_FragmentUI)UIPackage.CreateObject("Reputation", "FragmentUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_btn = (GButton)GetChildAt(4);
            m_list = (GList)GetChildAt(5);
            m_effect = (GGraph)GetChildAt(6);
            m_content = (GGroup)GetChildAt(7);
            m_show = GetTransitionAt(0);
        }
    }
}