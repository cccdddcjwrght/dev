/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.EnterScene
{
    public partial class UI_HeadRegion : GLabel
    {
        public Controller m_isMax;
        public GLoader m_region;
        public GGroup m_max;
        public GTextField m_title2;
        public const string URL = "ui://cxpm3jfbl63o17";

        public static UI_HeadRegion CreateInstance()
        {
            return (UI_HeadRegion)UIPackage.CreateObject("EnterScene", "HeadRegion");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_isMax = GetControllerAt(0);
            m_region = (GLoader)GetChildAt(0);
            m_max = (GGroup)GetChildAt(3);
            m_title2 = (GTextField)GetChildAt(5);
        }
    }
}