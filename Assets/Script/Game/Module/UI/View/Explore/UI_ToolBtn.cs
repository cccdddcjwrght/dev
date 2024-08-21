/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Explore
{
    public partial class UI_ToolBtn : GButton
    {
        public GImage m_bg;
        public const string URL = "ui://ow12is1hpm5b15";

        public static UI_ToolBtn CreateInstance()
        {
            return (UI_ToolBtn)UIPackage.CreateObject("Explore", "ToolBtn");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_bg = (GImage)GetChildAt(0);
        }
    }
}