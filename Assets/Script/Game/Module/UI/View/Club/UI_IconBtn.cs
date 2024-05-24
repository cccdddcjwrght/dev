/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Club
{
    public partial class UI_IconBtn : GButton
    {
        public Controller m_color;
        public const string URL = "ui://kgizakqqlu5m1e";

        public static UI_IconBtn CreateInstance()
        {
            return (UI_IconBtn)UIPackage.CreateObject("Club", "IconBtn");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_color = GetControllerAt(0);
        }
    }
}