/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.EnterScene
{
    public partial class UI_OpenAnim : GComponent
    {
        public Transition m_openanim2;
        public const string URL = "ui://cxpm3jfbd3z72m";

        public static UI_OpenAnim CreateInstance()
        {
            return (UI_OpenAnim)UIPackage.CreateObject("EnterScene", "OpenAnim");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_openanim2 = GetTransitionAt(0);
        }
    }
}