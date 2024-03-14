/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Player
{
    public partial class UI_EqPos : GButton
    {
        public Controller m_quality;
        public Controller m_eq;
        public GImage m_bg;
        public GTextField m_level;
        public const string URL = "ui://cmw7t1elk62213";

        public static UI_EqPos CreateInstance()
        {
            return (UI_EqPos)UIPackage.CreateObject("Player", "EqPos");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_quality = GetControllerAt(0);
            m_eq = GetControllerAt(1);
            m_bg = (GImage)GetChildAt(0);
            m_level = (GTextField)GetChildAt(3);
        }
    }
}