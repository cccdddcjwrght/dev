/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.SceneDecor
{
    public partial class UI_WallCell : GLabel
    {
        public Controller m_tab;
        public const string URL = "ui://04q21cn9sdk38";

        public static UI_WallCell CreateInstance()
        {
            return (UI_WallCell)UIPackage.CreateObject("SceneDecor", "WallCell");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_tab = GetControllerAt(0);
        }
    }
}