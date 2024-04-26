/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Flight
{
    public partial class UI_FlightUI : GComponent
    {
        public GButton m_Gold;
        public GButton m_Diamond;
        public GLoader m_Box;
        public const string URL = "ui://kh4ukj1qkxwz0";

        public static UI_FlightUI CreateInstance()
        {
            return (UI_FlightUI)UIPackage.CreateObject("Flight", "FlightUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_Gold = (GButton)GetChildAt(0);
            m_Diamond = (GButton)GetChildAt(1);
            m_Box = (GLoader)GetChildAt(2);
        }
    }
}