/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Hud
{
    public partial class UI_GetWorkerTipsUI : GComponent
    {
        public Controller m_type;
        public GLoader m_icon;
        public const string URL = "ui://clbwsjawhx7ug";

        public static UI_GetWorkerTipsUI CreateInstance()
        {
            return (UI_GetWorkerTipsUI)UIPackage.CreateObject("Hud", "GetWorkerTipsUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_type = GetControllerAt(0);
            m_icon = (GLoader)GetChildAt(1);
        }
    }
}