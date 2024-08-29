/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Explore
{
    public partial class UI_ExploreLvBody : GLabel
    {
        public GTextField m_old;
        public GTextField m_lv;
        public Transition m_t1;
        public const string URL = "ui://ow12is1hvj8z2n";

        public static UI_ExploreLvBody CreateInstance()
        {
            return (UI_ExploreLvBody)UIPackage.CreateObject("Explore", "ExploreLvBody");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_old = (GTextField)GetChildAt(3);
            m_lv = (GTextField)GetChildAt(4);
            m_t1 = GetTransitionAt(0);
        }
    }
}