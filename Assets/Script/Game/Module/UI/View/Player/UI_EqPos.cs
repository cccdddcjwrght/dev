/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Player
{
    public partial class UI_EqPos : GButton
    {
        public GImage m_bg;
        public const string URL = "ui://cmw7t1elk62213";

        public static UI_EqPos CreateInstance()
        {
            return (UI_EqPos)UIPackage.CreateObject("Player", "EqPos");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_bg = (GImage)GetChildAt(0);
        }
    }
}