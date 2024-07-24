/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Cookbook
{
    public partial class UI_CollectBg : GLabel
    {
        public Controller m_type;
        public GButton m_close;
        public const string URL = "ui://n2tgmsyuadfx26";

        public static UI_CollectBg CreateInstance()
        {
            return (UI_CollectBg)UIPackage.CreateObject("Cookbook", "CollectBg");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_type = GetControllerAt(0);
            m_close = (GButton)GetChildAt(4);
        }
    }
}