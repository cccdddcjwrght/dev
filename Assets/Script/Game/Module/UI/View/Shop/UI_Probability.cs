/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Shop
{
    public partial class UI_Probability : GComponent
    {
        public Controller m_show;
        public GLabel m_bg;
        public GLoader m_icon;
        public GList m_list;
        public Transition m_t0;
        public Transition m_hide;
        public const string URL = "ui://aphwhwgnlxylv";

        public static UI_Probability CreateInstance()
        {
            return (UI_Probability)UIPackage.CreateObject("Shop", "Probability");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_show = GetControllerAt(0);
            m_bg = (GLabel)GetChildAt(1);
            m_icon = (GLoader)GetChildAt(2);
            m_list = (GList)GetChildAt(4);
            m_t0 = GetTransitionAt(0);
            m_hide = GetTransitionAt(1);
        }
    }
}