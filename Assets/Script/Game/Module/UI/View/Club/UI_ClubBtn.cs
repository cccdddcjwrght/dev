/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Club
{
    public partial class UI_ClubBtn : GButton
    {
        public Controller m_color;
        public GLoader m_bg;
        public const string URL = "ui://kgizakqqlu5m1h";

        public static UI_ClubBtn CreateInstance()
        {
            return (UI_ClubBtn)UIPackage.CreateObject("Club", "ClubBtn");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_color = GetControllerAt(0);
            m_bg = (GLoader)GetChildAt(0);
        }
    }
}