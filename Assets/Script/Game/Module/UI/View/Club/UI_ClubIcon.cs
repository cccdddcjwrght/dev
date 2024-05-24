/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Club
{
    public partial class UI_ClubIcon : GLabel
    {
        public GLoader m_frame;
        public GLoader m_head;
        public const string URL = "ui://kgizakqqlu5m1g";

        public static UI_ClubIcon CreateInstance()
        {
            return (UI_ClubIcon)UIPackage.CreateObject("Club", "ClubIcon");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_frame = (GLoader)GetChildAt(0);
            m_head = (GLoader)GetChildAt(1);
        }
    }
}