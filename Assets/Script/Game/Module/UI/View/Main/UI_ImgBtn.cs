/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Main
{
    public partial class UI_ImgBtn : GButton
    {
        public GGraph m_center;
        public const string URL = "ui://ktixaqljgmj1t";

        public static UI_ImgBtn CreateInstance()
        {
            return (UI_ImgBtn)UIPackage.CreateObject("Main", "ImgBtn");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_center = (GGraph)GetChildAt(1);
        }
    }
}