/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Flight
{
    public partial class UI_FlightUI : GComponent
    {
        public GButton m_Gold;
        public GButton m_Diamond;
        public GButton m_rank;
        public GComponent m_rankTran;
        public GButton m_Box;
        public GButton m_Pet;
        public GComponent m_totalBtn;
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
            m_rank = (GButton)GetChildAt(2);
            m_rankTran = (GComponent)GetChildAt(3);
            m_Box = (GButton)GetChildAt(4);
            m_Pet = (GButton)GetChildAt(5);
            m_totalBtn = (GComponent)GetChildAt(6);
        }
    }
}