/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Cookbook
{
    public partial class UI_CookStar : GComponent
    {
        public Controller m_type;
        public const string URL = "ui://n2tgmsyur4i1a";

        public static UI_CookStar CreateInstance()
        {
            return (UI_CookStar)UIPackage.CreateObject("Cookbook", "CookStar");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_type = GetControllerAt(0);
        }
    }
}