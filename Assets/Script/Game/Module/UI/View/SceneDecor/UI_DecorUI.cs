/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.SceneDecor
{
    public partial class UI_DecorUI : GLabel
    {
        public GLoader m_loader;
        public const string URL = "ui://04q21cn9sdk36";

        public static UI_DecorUI CreateInstance()
        {
            return (UI_DecorUI)UIPackage.CreateObject("SceneDecor", "DecorUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_loader = (GLoader)GetChildAt(0);
        }
    }
}