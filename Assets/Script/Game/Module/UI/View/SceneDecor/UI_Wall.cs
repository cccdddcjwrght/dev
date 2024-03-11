/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.SceneDecor
{
    public partial class UI_Wall : GComponent
    {
        public Controller m_c1;
        public Controller m_c2;
        public Controller m_c3;
        public Controller m_c4;
        public Controller m_c5;
        public const string URL = "ui://04q21cn9sdk37";

        public static UI_Wall CreateInstance()
        {
            return (UI_Wall)UIPackage.CreateObject("SceneDecor", "Wall");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_c1 = GetControllerAt(0);
            m_c2 = GetControllerAt(1);
            m_c3 = GetControllerAt(2);
            m_c4 = GetControllerAt(3);
            m_c5 = GetControllerAt(4);
        }
    }
}