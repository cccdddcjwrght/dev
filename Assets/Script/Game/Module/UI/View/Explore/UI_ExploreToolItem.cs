/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Explore
{
    public partial class UI_ExploreToolItem : GComponent
    {
        public Controller m_quality;
        public Controller m_upcolor;
        public GTextField m_val;
        public GTextField m_next;
        public const string URL = "ui://ow12is1hpm5b1m";

        public static UI_ExploreToolItem CreateInstance()
        {
            return (UI_ExploreToolItem)UIPackage.CreateObject("Explore", "ExploreToolItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_quality = GetControllerAt(0);
            m_upcolor = GetControllerAt(1);
            m_val = (GTextField)GetChildAt(1);
            m_next = (GTextField)GetChildAt(2);
        }
    }
}