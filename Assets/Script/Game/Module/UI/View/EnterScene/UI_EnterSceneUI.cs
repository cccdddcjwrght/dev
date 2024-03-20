/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.EnterScene
{
    public partial class UI_EnterSceneUI : GComponent
    {
        public GGraph m___effect;
        public GLoader m_loader;
        public const string URL = "ui://cxpm3jfbicj20";

        public static UI_EnterSceneUI CreateInstance()
        {
            return (UI_EnterSceneUI)UIPackage.CreateObject("EnterScene", "EnterSceneUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m___effect = (GGraph)GetChildAt(0);
            m_loader = (GLoader)GetChildAt(1);
        }
    }
}