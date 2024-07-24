/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Cookbook
{
    public partial class UI_WorkStar : GLabel
    {
        public Controller m_type;
        public Transition m_t0;
        public const string URL = "ui://n2tgmsyuadfxz";

        public static UI_WorkStar CreateInstance()
        {
            return (UI_WorkStar)UIPackage.CreateObject("Cookbook", "WorkStar");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_type = GetControllerAt(0);
            m_t0 = GetTransitionAt(0);
        }
    }
}