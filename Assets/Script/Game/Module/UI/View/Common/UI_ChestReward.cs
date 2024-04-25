/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Common
{
    public partial class UI_ChestReward : GButton
    {
        public Controller m_hide;
        public const string URL = "ui://2w8thcm7udvn11";

        public static UI_ChestReward CreateInstance()
        {
            return (UI_ChestReward)UIPackage.CreateObject("Common", "ChestReward");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_hide = GetControllerAt(0);
        }
    }
}